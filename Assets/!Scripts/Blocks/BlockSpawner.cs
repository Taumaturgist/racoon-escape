using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private eBlockType _previousBlockType;
    private List<GameObject> _blocks = new();
    private GameObject[] _tileSet;
    private Vector3 _pos;
    private Quaternion _rot;
    private int _nextBlockType;


    public void Launch(BlockSpawnConfig blockSpawnConfig)
    {
        _pos = blockSpawnConfig.SpawnPointFirstBlock;
        _rot = Quaternion.identity;
        CreateFirstBlock(blockSpawnConfig, _pos, _rot);
        // Áðîêåð ñîîáùåíèé -> ÑreateFollowingBlock()
    }

    private void CreateFirstBlock(BlockSpawnConfig blockSpawnConfig, Vector3 pos, Quaternion rot)
    {
        var firstBlock = Instantiate(
                                blockSpawnConfig.Block,
                                pos,
                                rot,
                                transform);
        SetFirstBlockParameters(blockSpawnConfig, firstBlock);

        CreateTilesInFirstBlock(
                    blockSpawnConfig,
                    firstBlock.gameObject,
                    firstBlock.TilesCount,
                    pos,
                    rot);

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
                            GameObject firstBlock,
                            int tilesCountInFirstBlock,
                            Vector3 pos,
                            Quaternion rot)
    {
        var tilesInFirstBlock = new GameObject[tilesCountInFirstBlock];
        var crossroadCount = 0;
        var numberCrossroad = 3;
        int randomIndex;

        for (int i = 0; i < tilesInFirstBlock.Length - 1; i++)
        {
            randomIndex = GetRandomIndexForBlock(blockSpawnConfig.CityTiles.Length);
            if (crossroadCount < 2)
            {
                tilesInFirstBlock[i] = Instantiate(
                                   blockSpawnConfig.CityTiles[randomIndex],
                                   pos,
                                   rot,
                                   firstBlock.transform);
                if (randomIndex == numberCrossroad)
                    crossroadCount++;
            }
            else
            {
                randomIndex = GetRandomIndexForBlock(blockSpawnConfig.CityTiles.Length - 1);

                tilesInFirstBlock[i] = Instantiate(
                                    blockSpawnConfig.CityTiles[randomIndex],
                                    pos,
                                    rot,
                                    firstBlock.transform);
            }

            pos.z += blockSpawnConfig.OffsetZ;
        }
        _previousBlockType = eBlockType.City;
        _nextBlockType = UnityEngine.Random.Range((int)eBlockType.Desert, Enum.GetNames(typeof(eBlockType)).Length);
        switch (_nextBlockType)
        {
            case 1:
                tilesInFirstBlock[tilesInFirstBlock.Length - 1] = Instantiate(
                                                                    blockSpawnConfig.CityDesertTile,
                                                                    pos,
                                                                    rot,
                                                                    firstBlock.transform);
                break;
            case 2:
                tilesInFirstBlock[tilesInFirstBlock.Length - 1] = Instantiate(
                                                                    blockSpawnConfig.CityForestTile,
                                                                    pos,
                                                                    rot,
                                                                    firstBlock.transform);
                break;
            case 3:
                tilesInFirstBlock[tilesInFirstBlock.Length - 1] = Instantiate(
                                                                    blockSpawnConfig.CityHighwayTile,
                                                                    pos,
                                                                    rot,
                                                                    firstBlock.transform);
                break;
        }
        pos.z += blockSpawnConfig.OffsetZ;

        _previousBlockType = eBlockType.City;
    }
    private int GetRandomIndexForBlock(int upperBound)
    {
        return UnityEngine.Random.Range(0, upperBound);
    }

    private void CreateFollowingBlock(BlockSpawnConfig blockSpawnConfig, Vector3 pos, Quaternion rot)
    {
        var block = Instantiate(
                            blockSpawnConfig.Block,
                            pos,
                            rot,
                            transform);
        SetBlockParameters(blockSpawnConfig, block);
        CreateTilesInFollowingBlock(
                            blockSpawnConfig,
                            block.gameObject,
                            pos,
                            rot);
    }
    private void SetBlockParameters(BlockSpawnConfig blockSpawnConfig, Block block)
    {
        block.BlockID = GetBlockID();
        block.BlockType = GetBlockType();
        block.TilesCount = GetTilesCount(blockSpawnConfig);
    }
    private int GetBlockID()
    {
        return _blocks.Count;
    }
    private eBlockType GetBlockType()
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
        var tileCount = UnityEngine.Random.Range(
                                            blockSpawnConfig.MinTilesCountInBlock,
                                            blockSpawnConfig.MaxTilesCountInBlock + 1);
        return tileCount;
    }

    private void CreateTilesInFollowingBlock(
                                    BlockSpawnConfig blockSpawnConfig,
                                    GameObject block,
                                    Vector3 pos,
                                    Quaternion rot)
    {
        var tilesInBlock = new GameObject[block.GetComponent<Block>().TilesCount];
        var crossroadCount = 0;
        var numberCrossroad = 3;
        int randomIndex;
        var blockType = block.GetComponent<Block>().BlockType;
        switch (blockType)
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
                
        for (int i = 0; i < tilesInBlock.Length - 1; i++)
        {
            randomIndex = GetRandomIndexForBlock(_tileSet.Length);
            if (crossroadCount < 2)
            {
                tilesInBlock[i] = Instantiate(
                                   _tileSet[randomIndex],
                                   pos,
                                   rot,
                                   block.transform);
                if (randomIndex == numberCrossroad)
                    crossroadCount++;
            }
            else
            {
                randomIndex = GetRandomIndexForBlock(_tileSet.Length - 1);

                tilesInBlock[i] = Instantiate(
                                    _tileSet[randomIndex],
                                    pos,
                                    rot,
                                    block.transform);
            }

            pos.z += blockSpawnConfig.OffsetZ;
        }
        var nextBlockType = _previousBlockType;
        while (nextBlockType != _previousBlockType)
            nextBlockType = (eBlockType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(eBlockType)).Length);
        /*switch ((int) nextBlockType)
        {
            case 1:
                tilesInBlock[tilesInBlock.Length - 1] = Instantiate(
                                                                    blockSpawnConfig.CityDesertTile,
                                                                    pos,
                                                                    rot,
                                                                    block.transform);
                break;
            case 2:
                tilesInBlock[tilesInBlock.Length - 1] = Instantiate(
                                                                    blockSpawnConfig.CityForestTile,
                                                                    pos,
                                                                    rot,
                                                                    block.transform);
                break;
            case 3:
                tilesInBlock[tilesInBlock.Length - 1] = Instantiate(
                                                                    blockSpawnConfig.CityHighwayTile,
                                                                    pos,
                                                                    rot,
                                                                    block.transform);
                break;
            case 4:
                tilesInBlock[tilesInBlock.Length - 1] = Instantiate(
                                                                    blockSpawnConfig.CityHighwayTile,
                                                                    pos,
                                                                    rot,
                                                                    block.transform);
                break;
        }
        */
        pos.z += blockSpawnConfig.OffsetZ;

        _previousBlockType = blockType;
    }
    private void BlockDestroying(GameObject block)
    {
        Destroy(block);
    }
}