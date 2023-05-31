/* 
 *      Отписка после проигрыша _disposableTrigger.Clear();
 */

using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private BlockSpawnConfig _blockSpawnConfig;
    private BuildingSpawnConfig _buildingSpawnConfig;

    private CompositeDisposable _disposable = new();
    private eBlockType _previousBlockType, _nextBlockType;

    private List<GameObject> _blocks = new();

    private Vector3 _pos;
    private Quaternion _rot;

    private BoxCollider[] _transitionTileColliders;

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
        _transitionTileColliders = new BoxCollider[2];

        for (int i = 0; i < 2; i++)
            CreateBlock(i);
    }
    
    private void CreateBlock(int transitionTileNumber)
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

        _blocks.Add(block.gameObject);
        _blockCount = _blocks.Count;

        _transitionTileColliders[transitionTileNumber] = tileSpawner.GetTransitionTileCollider();
        CheckTrigger(_transitionTileColliders[transitionTileNumber]);
    }
    private void CheckTrigger(Collider trigger)
    {
        trigger.OnTriggerEnterAsObservable()
            .Where(t => t.gameObject.CompareTag("Player"))
            .Subscribe(other =>
            {
                CheckBlockRemoval();
                CreateBlock(0);
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