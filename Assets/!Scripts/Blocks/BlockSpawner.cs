/* 
*      Написать отписку после проигрыша _disposableTrigger.Clear();
 */

using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private BlockSpawnConfig _blockSpawnConfig;
    private CompositeDisposable _disposableTrigger = new();
    private eBlockType _previousBlockType, _nextBlockType;

    private List<GameObject> _blocks = new();
    private GameObject[] _tileSet;

    private Vector3 _pos;
    private Quaternion _rot;
    private GameObject _transitionTile;
    private int _blockCount;
    private bool _isFirstBlock;

    public void Launch(BlockSpawnConfig blockSpawnConfig)
    {
        _blockSpawnConfig = blockSpawnConfig;
        _pos = blockSpawnConfig.SpawnPointFirstBlock;
        _rot = Quaternion.identity;
        _isFirstBlock = true;
        _blockCount = 0;

        CreateBlock();
    }

    private void CreateBlock()
    {
        var block = Instantiate(_blockSpawnConfig.Block, _pos, _rot, transform);

        if (_isFirstBlock)
        {
            GetFirstBlockParameters(block);
            _isFirstBlock = false;
        }
        else
            GetBlockParameters(block);

        CreateTiles(block);

        _blocks.Add(block.gameObject);
        _blockCount = _blocks.Count;

        var transitionTileCollider = _transitionTile.GetComponent<BoxCollider>();
        CheckTrigger(transitionTileCollider);
    }

    private void GetFirstBlockParameters(Block block)
    {
        //block.BlockID = 0;
        block.BlockType = eBlockType.City;
        block.TilesCount = _blockSpawnConfig.TilesCountInFirstBlock;
    }
    private void GetBlockParameters(Block block)
    {
        //block.BlockID = GetBlockID();
        block.BlockType = _nextBlockType;
        block.TilesCount = GetTilesCount();
    }
    //private int GetBlockID()
    //{
    //    return _blocks.Count;
    //}
    private eBlockType GetBlockType()
    {
        var randomIndex = (int)_previousBlockType;

        while (randomIndex == (int)_previousBlockType)
        {
            var enumLen = Enum.GetNames(typeof(eBlockType)).Length;
            randomIndex = GetRandomIndex(0, enumLen);
        }

        return (eBlockType)randomIndex;
    }
    private int GetTilesCount()
    {
        var tilesCount = GetRandomIndex(_blockSpawnConfig.MinTilesCount, _blockSpawnConfig.MaxTilesCount + 1);
        return tilesCount;
    }
    private int GetRandomIndex(int underBound, int upperBound)
    {
        return UnityEngine.Random.Range(underBound, upperBound);
    }

    private void CreateTiles(Block block)
    {
        var tiles = new GameObject[block.TilesCount];
        var blockType = block.BlockType;

        switch (blockType)
        {
            case eBlockType.City:
                _tileSet = _blockSpawnConfig.CityTiles;
                break;
            case eBlockType.Desert:
                _tileSet = _blockSpawnConfig.DesertTiles;
                break;
            case eBlockType.Forest:
                _tileSet = _blockSpawnConfig.ForestTiles;
                break;
            case eBlockType.Highway:
                _tileSet = _blockSpawnConfig.HighwayTiles;
                break;
        }

        var crossroadCount = 0;

        int randomIndex;
        for (int i = 0; i < tiles.Length - 1; i++)
        {
            randomIndex = GetRandomIndex(0, _tileSet.Length);
            if (crossroadCount < 2)
            {
                tiles[i] = Instantiate(_tileSet[randomIndex], _pos, _rot, block.transform);
                if (randomIndex == _blockSpawnConfig.CrossroadNumberInCity)
                    crossroadCount++;
            }
            else
            {
                randomIndex = GetRandomIndex(0, _tileSet.Length - 1);

                tiles[i] = Instantiate(_tileSet[randomIndex], _pos, _rot, block.transform);
            }

            _pos.z += _blockSpawnConfig.OffsetZ;
        }

        _previousBlockType = blockType;
        _nextBlockType = GetBlockType();

        _transitionTile = GetTransitionTile(block, tiles);

        _pos.z += _blockSpawnConfig.OffsetZ;

    }
    private GameObject GetTransitionTile(Block block, GameObject[] tiles)
    {
        var lastIndex = tiles.Length - 1;
        switch (_previousBlockType)
        {
            case eBlockType.City:
                switch (_nextBlockType)
                {
                    case eBlockType.Desert:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.CityDesertTile, block.gameObject);
                        break;
                    case eBlockType.Forest:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.CityForestTile, block.gameObject);
                        break;
                    case eBlockType.Highway:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.CityHighwayTile, block.gameObject);
                        break;
                }
                break;
            case eBlockType.Desert:
                switch (_nextBlockType)
                {
                    case eBlockType.City:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.DesertCityTile, block.gameObject);
                        break;
                    case eBlockType.Forest:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.DesertForestTile, block.gameObject);
                        break;
                    case eBlockType.Highway:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.DesertHighwayTile, block.gameObject);
                        break;
                }
                break;
            case eBlockType.Forest:
                switch (_nextBlockType)
                {
                    case eBlockType.City:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.ForestCityTile, block.gameObject);
                        break;
                    case eBlockType.Desert:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.ForestDesertTile, block.gameObject);
                        break;
                    case eBlockType.Highway:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.ForestHighwayTile, block.gameObject);
                        break;
                }
                break;
            case eBlockType.Highway:
                switch (_nextBlockType)
                {
                    case eBlockType.City:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.HighwayCityTile, block.gameObject);
                        break;
                    case eBlockType.Desert:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.HighwayDesertTile, block.gameObject);
                        break;
                    case eBlockType.Forest:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.HighwayForestTile, block.gameObject);
                        break;
                }
                break;
        }

        return tiles[lastIndex];
    }
    private GameObject CreateTransitionTile(GameObject obj, GameObject parent)
    {
        return Instantiate(obj, _pos, obj.transform.rotation, parent.transform);
    }
    
    private void CheckTrigger(Collider trigger)
    {
        //Debug.Log(_disposable.Count.ToString());
        trigger.OnTriggerEnterAsObservable()
            .Where(t => t.gameObject.CompareTag("Player"))
            .Subscribe(other =>
            {
                CheckBlockRemoval();
                CreateBlock();
            }).AddTo(_disposableTrigger);
    }

    private void CheckBlockRemoval()
    {
        if (_blockCount > 3)
        {
            var removableBlock = _blocks[0];
            _blocks.Remove(_blocks[0]);
            Destroy(removableBlock);
        }
    }
}