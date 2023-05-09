/*  Задачи
 *      1) Учесть при спавне количество перекрестков в блоке City в последующих блоках
 *      2) Исправить спавн переходных тайлов в последующих блоках
 *      3) Создавать блоки с помощью OnTriggerEnterAsObservable()
 *      4) Уничтожать блоки можно с помощью метода OnTriggerExitAsObservable()
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private eBlockType _previousBlockType;
    private eBlockType _nextBlockType;
    private List<GameObject> _blocks = new();
    private GameObject[] _tileSet;
    private Vector3 _pos;
    private Quaternion _rot = Quaternion.identity;

    public void Launch(BlockSpawnConfig blockSpawnConfig)
    {
        _pos = blockSpawnConfig.SpawnPointFirstBlock;
        CreateFirstBlock(blockSpawnConfig);
        CreateFollowingBlock(blockSpawnConfig);
    }

    private void CreateFirstBlock(BlockSpawnConfig blockSpawnConfig)
    {
        var firstBlock = Instantiate(
                                blockSpawnConfig.Block,
                                _pos,
                                _rot,
                                transform);
        SetFirstBlockParameters(blockSpawnConfig, firstBlock);

        CreateTilesInFirstBlock(
                    blockSpawnConfig,
                    firstBlock);

        _blocks.Add(firstBlock.gameObject);
    }

    private void SetFirstBlockParameters(BlockSpawnConfig blockSpawnConfig, Block firstBlock)
    {
        firstBlock.BlockID = 0;
        firstBlock.BlockType = eBlockType.City;
        firstBlock.TilesCount = blockSpawnConfig.TilesCountInFirstBlock;
    }

    private void CreateTilesInFirstBlock(
                            BlockSpawnConfig blockSpawnConfig,
                            Block firstBlock)
    {
        var tilesInFirstBlock = new GameObject[firstBlock.TilesCount];
        var crossroadCount = 0;
        var сrossroadNumberInInspector = 3;
        int randomIndex;

        for (int i = 0; i < tilesInFirstBlock.Length - 1; i++)
        {
            randomIndex = GetRandomIndexForBlock(blockSpawnConfig.CityTiles.Length);
            if (crossroadCount < 2)
            {
                tilesInFirstBlock[i] = Instantiate(
                                   blockSpawnConfig.CityTiles[randomIndex],
                                   _pos,
                                   _rot,
                                   firstBlock.gameObject.transform);
                if (randomIndex == сrossroadNumberInInspector)
                    crossroadCount++;
            }
            else
            {
                randomIndex = GetRandomIndexForBlock(blockSpawnConfig.CityTiles.Length - 1);

                tilesInFirstBlock[i] = Instantiate(
                                    blockSpawnConfig.CityTiles[randomIndex],
                                    _pos,
                                    _rot,
                                    firstBlock.gameObject.transform);
            }

            _pos.z += blockSpawnConfig.OffsetZ;
        }
        _previousBlockType = eBlockType.City;
        _nextBlockType = GetUniqueBlockType();
        switch ((int)_nextBlockType)
        {
            case 1:
                tilesInFirstBlock[tilesInFirstBlock.Length - 1] = Instantiate(
                                                                    blockSpawnConfig.CityDesertTile,
                                                                    _pos,
                                                                    _rot,
                                                                    firstBlock.transform);
                break;
            case 2:
                tilesInFirstBlock[tilesInFirstBlock.Length - 1] = Instantiate(
                                                                    blockSpawnConfig.CityForestTile,
                                                                    _pos,
                                                                    _rot,
                                                                    firstBlock.transform);
                break;
            case 3:
                tilesInFirstBlock[tilesInFirstBlock.Length - 1] = Instantiate(
                                                                    blockSpawnConfig.CityHighwayTile,
                                                                    _pos,
                                                                    _rot,
                                                                    firstBlock.transform);
                break;
        }
        _pos.z += blockSpawnConfig.OffsetZ;

        _previousBlockType = eBlockType.City;
    }
    private int GetRandomIndexForBlock(int upperBound)
    {
        return UnityEngine.Random.Range(0, upperBound);
    }

    private void CreateFollowingBlock(BlockSpawnConfig blockSpawnConfig)
    {
        var block = Instantiate(
                            blockSpawnConfig.Block,
                            _pos,
                            _rot,
                            transform);
        SetBlockParameters(blockSpawnConfig, block);
        CreateTilesInFollowingBlock(
                            blockSpawnConfig,
                            block);
    }
    private void SetBlockParameters(BlockSpawnConfig blockSpawnConfig, Block block)
    {
        block.BlockID = GetBlockID();
        block.BlockType = _nextBlockType;
        block.TilesCount = GetTilesCount(blockSpawnConfig);
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
    private int GetTilesCount(BlockSpawnConfig blockSpawnConfig)
    {
        var tilesCount = UnityEngine.Random.Range(
                                            blockSpawnConfig.MinTilesCountInBlock,
                                            blockSpawnConfig.MaxTilesCountInBlock + 1);
        return tilesCount;
    }

    private void CreateTilesInFollowingBlock(
                                    BlockSpawnConfig blockSpawnConfig,
                                    Block block)
    {
        var tilesInBlock = new GameObject[block.TilesCount];

        switch (block.BlockType)
        {
            case eBlockType.City:
                _tileSet = blockSpawnConfig.CityTiles;
                break;
            case eBlockType.Desert:
                _tileSet = blockSpawnConfig.DesertTiles;
                break;
            case eBlockType.Forest:
                _tileSet = blockSpawnConfig.ForestTiles;
                break;
            case eBlockType.Highway:
                _tileSet = blockSpawnConfig.HighwayTiles;
                break;
        }

        var crossroadCount = 0;
        var сrossroadNumberInInspector = 3;

        int randomIndex;
        for (int i = 0; i < tilesInBlock.Length - 1; i++)
        {
            randomIndex = GetRandomIndexForBlock(_tileSet.Length);
            if (crossroadCount < 2)
            {
                tilesInBlock[i] = Instantiate(
                                   _tileSet[randomIndex],
                                   _pos,
                                   _rot,
                                   block.transform);
                if (randomIndex == сrossroadNumberInInspector)
                    crossroadCount++;
            }
            else
            {
                randomIndex = GetRandomIndexForBlock(_tileSet.Length - 1);

                tilesInBlock[i] = Instantiate(
                                    _tileSet[randomIndex],
                                    _pos,
                                    _rot,
                                    block.transform);
            }

            _pos.z += blockSpawnConfig.OffsetZ;
        }
        var _nextBlockType = _previousBlockType;
        while (_nextBlockType != _previousBlockType)
            _nextBlockType = (eBlockType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(eBlockType)).Length);
        /*switch ((int) nextBlockType)
        {
            case 1:
                tilesInBlock[tilesInBlock.Length - 1] = Instantiate(
                                                                    blockSpawnConfig.CityDesertTile,
                                                                    _pos,
                                                                    _rot,
                                                                    block.transform);
                break;
            case 2:
                tilesInBlock[tilesInBlock.Length - 1] = Instantiate(
                                                                    blockSpawnConfig.CityForestTile,
                                                                    _pos,
                                                                    _rot,
                                                                    block.transform);
                break;
            case 3:
                tilesInBlock[tilesInBlock.Length - 1] = Instantiate(
                                                                    blockSpawnConfig.CityHighwayTile,
                                                                    _pos,
                                                                    _rot,
                                                                    block.transform);
                break;
            case 4:
                tilesInBlock[tilesInBlock.Length - 1] = Instantiate(
                                                                    blockSpawnConfig.CityHighwayTile,
                                                                    _pos,
                                                                    _rot,
                                                                    block.transform);
                break;
        }
        */
        _pos.z += blockSpawnConfig.OffsetZ;

        _previousBlockType = block.BlockType;
    }
    private void DestroyBlock(GameObject block)
    {
        Destroy(block);
    }
}