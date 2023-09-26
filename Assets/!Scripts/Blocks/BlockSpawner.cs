/*
 *      Unsubscribing after losing _disposableTrigger.Clear();
 */

using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private BlockSpawnConfig _blockSpawnConfig;
    private BuildingSpawnConfig _buildingSpawnConfig;
    private TrafficSpawnConfig _trafficSpawnConfig;

    private CompositeDisposable _disposable = new();
    private eBlockType _previousBlockType, _nextBlockType;

    private List<Block> _blocks = new();

    private Vector3 _pos;
    private Quaternion _rot;

    private BoxCollider[] _transitionTileColliders;

    private int _blockCount;
    private bool _isFirstBlock;

    public void Launch(BlockSpawnConfig blockSpawnConfig, BuildingSpawnConfig buildingSpawnConfig,
        TrafficSpawnConfig trafficSpawnConfig)
    {
        _blockSpawnConfig = blockSpawnConfig;
        _buildingSpawnConfig = buildingSpawnConfig;
        _trafficSpawnConfig = trafficSpawnConfig;

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
        }
        else
            block.GetBlockParameters(_nextBlockType);

        var tileSpawner = Instantiate(_blockSpawnConfig.TileSpawner, _pos, _rot, block.transform);
        tileSpawner.Launch(
            _blockSpawnConfig,
            _buildingSpawnConfig,
            _trafficSpawnConfig,
            block,
            ref _previousBlockType,
            ref _nextBlockType,
            ref _pos, _rot,
            _isFirstBlock);
        
        _isFirstBlock = false;

        _blocks.Add(block);
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
                MessageBroker
                    .Default
                    .Publish(new OnAmbientThemeSwitchMessage(
                        _blocks.Count > 2 ? _blocks[2].BlockType : _blocks[1].BlockType
                    ));

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
            Destroy(removableBlock.gameObject);
        }
    }
}