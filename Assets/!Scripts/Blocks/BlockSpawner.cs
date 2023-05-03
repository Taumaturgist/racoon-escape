using System;
using System.Collections.Generic;
using UnityEngine;
public class BlockSpawner : MonoBehaviour
{
    private eBlockType _previousBlockType;
    private List<GameObject> _blocks = new();
    private Vector3 _pos;
    private Quaternion _rot;
    private int _nextBlockType;

    public void Launch(BlockSpawnConfig blockSpawnConfig)
    {
        _pos = blockSpawnConfig.SpawnPointFirstBlock;
        _rot = Quaternion.identity;
        CreateFirstBlock(blockSpawnConfig, _pos, _rot);
    }

    private void CreateFirstBlock(BlockSpawnConfig blockSpawnConfig, Vector3 pos, Quaternion rot)
    {
        var firstBlock = Instantiate(
                                blockSpawnConfig.Block,
                                pos,
                                rot,
                                transform);
        SetFirstBlockParameters(blockSpawnConfig, firstBlock);

        var tilesCountInFirstBlock = firstBlock.TilesCount;
        CreateTilesInFirstBlock(
                    blockSpawnConfig,
                    firstBlock.gameObject,
                    tilesCountInFirstBlock,
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
            randomIndex = GetRandomIndexForFirstBlock(blockSpawnConfig.CityTiles.Length);
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
                randomIndex = GetRandomIndexForFirstBlock(blockSpawnConfig.CityTiles.Length - 1);

                tilesInFirstBlock[i] = Instantiate(
                                    blockSpawnConfig.CityTiles[randomIndex],
                                    pos,
                                    rot,
                                    firstBlock.transform);
            }

            pos.z += blockSpawnConfig.OffsetZ;
        }
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
    }
    private int GetRandomIndexForFirstBlock(int upperBound)
    {
        return UnityEngine.Random.Range(0, upperBound);
    }

    //private void OnTriggerEnter(Collider other)
    //{
        
    //}

    //private void CreateFollowingBlock(BlockSpawnConfig blockSpawnConfig, Vector3 pos, Quaternion rot)
    //{
        
    //}
    //private void BlockCreation()
    //{
    //    Vector3 _position = Vector3.zero;
    //    Quaternion _rotation = Quaternion.identity;

    //    var newBlock = Instantiate(_block, _position, _rotation);

    //    newBlock.AddComponent<Block>();
    //    var newBlockComponent = newBlock.GetComponent<Block>();

    //    newBlockComponent.blockID = SetBlockID();
    //    newBlockComponent.blockType = SetBlockType();
    //    newBlockComponent.tilesCount = SetTilesCount();

    //    // Создать тайлы внутри блока
    //    for (int tileNumber = 0; tileNumber < newBlockComponent.tilesCount; tileNumber++)
    //    {
    //        Vector3 offset = new Vector3(0f, 0f, tileNumber * offsetZ);
    //    }
    //    blocks.Add(newBlock);
    //}
    //private void BlockDestroying(GameObject block)
    //{
    //    Destroy(block);
    //}

    //private int SetBlockID()
    //{
    //    return blocks.Count;
    //}
    //private int SetBlockType()
    //{
    //    return 0;
    //}
    //private int SetTilesCount()
    //{
    //    var tileCount = Random.Range(minTileCountInBlock, maxTileCountInBlock + 1);
    //    return tileCount;
    //}
}