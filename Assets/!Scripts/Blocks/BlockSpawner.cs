/* 
 *      на старте должны создатьс€ 2 блока.
 *      блок 3 создаетс€ при срабатывании триггера на блоке 1.
 *      блок 4 - на блоке 2, и т.д.
 *      сейчас новый блок создаетс€ пр€мо перед игроком.
 * 
 * 
 * 
 * 
 *      ќтделить случай города от остальных при создании тайлов (не более 2 перекрестков)
 *      Ќаписать отписку после проигрыша _disposableTrigger.Clear();
 */

using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private BlockSpawnConfig _blockSpawnConfig;
    private BuildingSpawnConfig _buildingSpawnConfig;

    private eBlockType _previousBlockType, _nextBlockType;
    private CompositeDisposable _disposable = new();

    private List<GameObject> _blocks = new();

    private Vector3 _pos;
    private Quaternion _rot;

    private BoxCollider _transitionTileCollider;

    private int _blockCount;
    private bool _isFirstBlock;

    public void Launch(BlockSpawnConfig blockSpawnConfig, BuildingSpawnConfig buildingSpawnConfig)
    {
        _blockSpawnConfig = blockSpawnConfig;
        _buildingSpawnConfig = buildingSpawnConfig;

        _pos = blockSpawnConfig.SpawnPointFirstBlock;
        _rot = Quaternion.identity;
        _blockCount = 0;
        _isFirstBlock = true;

        CreateBlock();
    }

    private void CreateBlock()
    {
        var block = Instantiate(_blockSpawnConfig.Block, _pos, _rot, transform);
        block.Launch(_blockSpawnConfig);

        if (_isFirstBlock)
        {
            block.GetFirstBlockParameters();
            _isFirstBlock = false;
        }
        else
            block.GetBlockParameters(_nextBlockType);

        var tileSpawner = Instantiate(_blockSpawnConfig.TileSpawner, _pos, _rot, block.transform);
        tileSpawner.Launch(_blockSpawnConfig, _buildingSpawnConfig);

        tileSpawner.CreateTiles(block, ref _previousBlockType, ref _nextBlockType, ref _pos, _rot);
        _transitionTileCollider = tileSpawner.GetTransitionTileCollider();

        _blocks.Add(block.gameObject);
        _blockCount = _blocks.Count;

        CheckTrigger(_transitionTileCollider);
    }

    private void CheckTrigger(Collider trigger)
    {
        trigger.OnTriggerEnterAsObservable()
            .Where(t => t.gameObject.CompareTag("Player"))
            .Subscribe(other =>
            {
                CheckBlockRemoval();
                CreateBlock();
            }).AddTo(_disposable);
    }
    private void CheckBlockRemoval()
    {
        if (_blockCount > 2)
        {
            var removableBlock = _blocks[0];
            _blocks.Remove(_blocks[0]);
            Destroy(removableBlock);
        }
    }
}