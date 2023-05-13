/*  Задачи
 *      2) Создавать блоки с помощью OnTriggerEnterAsObservable()
 *      3) Уничтожать блоки можно с помощью метода OnTriggerExitAsObservable()
 *      4) Рефактор кода
 */

using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private BlockSpawnConfig _blockSpawnConfig;
    private eBlockType _previousBlockType, _nextBlockType;
    private List<GameObject> _blocks = new();
    private GameObject[] _tileSet;
    private Vector3 _pos;
    private Quaternion _rot;
    private CompositeDisposable _disposable = new CompositeDisposable();

    public void Launch(BlockSpawnConfig blockSpawnConfig)
    {
        _blockSpawnConfig = blockSpawnConfig;
        _pos = blockSpawnConfig.SpawnPointFirstBlock;
        _rot = Quaternion.identity;

        CreateFirstBlock();
    }

    private void CreateFirstBlock()
    {
        var firstBlock = Instantiate(_blockSpawnConfig.Block, _pos, _rot, transform);

        SetFirstBlockParameters(firstBlock);
        CreateTilesInFirstBlock(firstBlock);

        _blocks.Add(firstBlock.gameObject);
    }
    private void SetFirstBlockParameters(Block firstBlock)
    {
        firstBlock.BlockID = 0;
        firstBlock.BlockType = eBlockType.City;
        firstBlock.TilesCount = _blockSpawnConfig.TilesCountInFirstBlock;
    }
    private void CreateTilesInFirstBlock(Block firstBlock)
    {
        var tiles = new GameObject[firstBlock.TilesCount];
        var crossroadCount = 0;
        int randomIndex;

        for (int i = 0; i < tiles.Length - 1; i++)
        {
            randomIndex = GetRandomIndex(_blockSpawnConfig.CityTiles.Length);
            if (crossroadCount < 2)
            {
                tiles[i] = Instantiate(_blockSpawnConfig.CityTiles[randomIndex], _pos, _rot, firstBlock.transform);
                if (randomIndex == _blockSpawnConfig.CrossroadNumberInCity)
                    crossroadCount++;
            }
            else
            {
                randomIndex = GetRandomIndex(_blockSpawnConfig.CityTiles.Length - 1);

                tiles[i] = Instantiate(_blockSpawnConfig.CityTiles[randomIndex], _pos, _rot, firstBlock.transform);
            }

            _pos.z += _blockSpawnConfig.OffsetZ;
        }

        _previousBlockType = firstBlock.BlockType;
        _nextBlockType = GetUniqueBlockType();

        var transitionTile = TransitionTile(firstBlock, tiles);
        var transitionTileCollider = transitionTile.GetComponent<BoxCollider>();

        _pos.z += _blockSpawnConfig.OffsetZ;

        CheckTrigger(transitionTileCollider);
    }
    private int GetRandomIndex(int upperBound)
    {
        return UnityEngine.Random.Range(0, upperBound);
    }

    private void CreateFollowingBlock()
    {
        var block = Instantiate(_blockSpawnConfig.Block, _pos, _rot, transform);
        SetBlockParameters(block);
        CreateTilesInFollowingBlock(block);
    }
    private void SetBlockParameters(Block block)
    {
        block.BlockID = GetBlockID();
        block.BlockType = _nextBlockType;
        block.TilesCount = GetTilesCount();
    }
    private int GetBlockID()
    {
        return _blocks.Count;
    }
    private eBlockType GetUniqueBlockType()
    {
        var randomIndex = (int)_previousBlockType;

        while (randomIndex == (int)_previousBlockType)
        {
            var enumLen = Enum.GetNames(typeof(eBlockType)).Length;
            randomIndex = UnityEngine.Random.Range(0, enumLen);
        }

        return (eBlockType)randomIndex;
    }
    private int GetTilesCount()
    {
        var tilesCount = UnityEngine.Random.Range(_blockSpawnConfig.MinTilesCountInBlock, _blockSpawnConfig.MaxTilesCountInBlock + 1);
        return tilesCount;
    }
    private void CreateTilesInFollowingBlock(Block block)
    {
        var tiles = new GameObject[block.TilesCount];

        switch (block.BlockType)
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
            randomIndex = GetRandomIndex(_tileSet.Length);
            if (crossroadCount < 2)
            {
                tiles[i] = Instantiate(_tileSet[randomIndex], _pos, _rot, block.transform);
                if (randomIndex == _blockSpawnConfig.CrossroadNumberInCity)
                    crossroadCount++;
            }
            else
            {
                randomIndex = GetRandomIndex(_tileSet.Length - 1);

                tiles[i] = Instantiate(_tileSet[randomIndex], _pos, _rot, block.transform);
            }

            _pos.z += _blockSpawnConfig.OffsetZ;
        }

        _previousBlockType = block.BlockType;
        _nextBlockType = GetUniqueBlockType();

        var transitionTile = TransitionTile(block, tiles);
        var transitionTileCollider = transitionTile.GetComponent<BoxCollider>();

        _pos.z += _blockSpawnConfig.OffsetZ;

        CheckTrigger(transitionTileCollider);
    }
    private GameObject TransitionTile(Block block, GameObject[] tiles)
    {
        var lastIndex = tiles.Length - 1;
        switch ((int)_previousBlockType)
        {
            case 0:
                switch ((int)_nextBlockType)
                {
                    case 1:
                        tiles[lastIndex] = Instantiate(_blockSpawnConfig.CityDesertTile, _pos, _blockSpawnConfig.CityDesertTile.transform.rotation, block.transform);
                        break;
                    case 2:
                        tiles[lastIndex] = Instantiate(_blockSpawnConfig.CityForestTile, _pos, _blockSpawnConfig.CityForestTile.transform.rotation, block.transform);
                        break;
                    case 3:
                        tiles[lastIndex] = Instantiate(_blockSpawnConfig.CityHighwayTile, _pos, _blockSpawnConfig.CityHighwayTile.transform.rotation, block.transform);
                        break;
                }
                break;
            case 1:
                switch ((int)_nextBlockType)
                {
                    case 0:
                        tiles[lastIndex] = Instantiate(_blockSpawnConfig.DesertCityTile, _pos, _blockSpawnConfig.DesertCityTile.transform.rotation, block.transform);
                        break;
                    case 2:
                        tiles[lastIndex] = Instantiate(_blockSpawnConfig.DesertForestTile, _pos, _blockSpawnConfig.DesertForestTile.transform.rotation, block.transform);
                        break;
                    case 3:
                        tiles[lastIndex] = Instantiate(_blockSpawnConfig.DesertHighwayTile, _pos, _blockSpawnConfig.DesertHighwayTile.transform.rotation, block.transform);
                        break;
                }
                break;
            case 2:
                switch ((int)_nextBlockType)
                {
                    case 0:
                        tiles[lastIndex] = Instantiate(_blockSpawnConfig.ForestCityTile, _pos, _blockSpawnConfig.ForestCityTile.transform.rotation, block.transform);
                        break;
                    case 1:
                        tiles[lastIndex] = Instantiate(_blockSpawnConfig.ForestDesertTile, _pos, _blockSpawnConfig.ForestDesertTile.transform.rotation, block.transform);
                        break;
                    case 3:
                        tiles[lastIndex] = Instantiate(_blockSpawnConfig.ForestHighwayTile, _pos, _blockSpawnConfig.ForestHighwayTile.transform.rotation, block.transform);
                        break;
                }
                break;
            case 3:
                switch ((int)_nextBlockType)
                {
                    case 0:
                        tiles[lastIndex] = Instantiate(_blockSpawnConfig.HighwayCityTile, _pos, _blockSpawnConfig.HighwayCityTile.transform.rotation, block.transform);
                        break;
                    case 1:
                        tiles[lastIndex] = Instantiate(_blockSpawnConfig.HighwayDesertTile, _pos, _blockSpawnConfig.HighwayDesertTile.transform.rotation, block.transform);
                        break;
                    case 2:
                        tiles[lastIndex] = Instantiate(_blockSpawnConfig.HighwayForestTile, _pos, _blockSpawnConfig.HighwayForestTile.transform.rotation, block.transform);
                        break;
                }
                break;
        }

        return tiles[lastIndex];
    }
    
    private void CheckTrigger(Collider trigger)
    {
        trigger.OnTriggerEnterAsObservable()
            //.First()  
            .Where(t => t.gameObject.CompareTag("Player"))
            .Subscribe(other =>
            {
                CreateFollowingBlock();
            }).AddTo(_disposable);

        _disposable.Clear();
    }

    private void DestroyBlock(GameObject block)
    {
        Destroy(block);
    }
}