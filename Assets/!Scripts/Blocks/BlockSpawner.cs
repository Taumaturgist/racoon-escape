using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BlockSpawner : MonoBehaviour
{
    private ApplicationStartUp _applicationStartUp;
    private BlockSpawnConfig _blockSpawnConfig;
    private List<GameObject> _blocks = new();
    private eBlockType _previousBlockType;
    private int _previousTileType;

    private void Awake()
    {
        GetBlockSpawnConfig();
        CreateFirstBlock();
    }

    private void GetBlockSpawnConfig()
    {
        _applicationStartUp = FindObjectOfType<ApplicationStartUp>();
        _blockSpawnConfig = _applicationStartUp.BlockSpawnConfig;
    }
    private void CreateFirstBlock()
    {
        var pos = _blockSpawnConfig.SpawnPointFirstBlock;
        var rot = Quaternion.identity;

        var firstBlock = Instantiate(
                                _blockSpawnConfig.Block,
                                pos,
                                rot,
                                transform);
        firstBlock.AddComponent<Block>();
        var firstBlockComponent = firstBlock.GetComponent<Block>();
        SetFirstBlockParameters(firstBlockComponent);

        var tilesCountInFirstBlock = firstBlockComponent.tilesCount;
        CreateTilesInFirstBlock(
                    firstBlock,
                    tilesCountInFirstBlock,
                    pos,
                    rot);

        _blocks.Add(firstBlock);
    }

    private void SetFirstBlockParameters(Block firstBlockComponent)
    {
        firstBlockComponent.blockID = 0;
        firstBlockComponent.blockType = eBlockType.City;
        firstBlockComponent.tilesCount = _blockSpawnConfig.TilesCountInFirstBlock;
    }

    private void CreateTilesInFirstBlock(
                            GameObject firstBlock, 
                            int tilesCountInFirstBlock, 
                            Vector3 pos, 
                            Quaternion rot)
    {
        var tilesInFirstBlock = new GameObject[tilesCountInFirstBlock];

        var crossroadExists = false;
        var numberCrossroad = 3;

        var previousTileType = -1;
        for (int i = 0; i < tilesInFirstBlock.Length; i++)
        {
            // Не спавнить одинаковые тайлы
            var randomIndex = GetRandomIndexForFirstBlock();
            tilesInFirstBlock[i] = Instantiate(
                                               _blockSpawnConfig.CityTiles[randomIndex],
                                               pos,
                                               rot,
                                               firstBlock.transform);
            //if (randomIndex != _previousTileType)
            //{
            //    if ((randomIndex == numberCrossroad) && (crossroadExists == true))
            //    { }
            //    else
            //    {
            //        if (randomIndex == numberCrossroad)
            //            crossroadExists = true;

            //        tilesInFirstBlock[i] = Instantiate(
            //                                    _blockSpawnConfig.CityTiles[randomIndex],
            //                                    pos,
            //                                    rot,
            //                                    firstBlock.transform);
            //    }
            //}
            //else
            //{
            //    randomIndex = GetRandomIndexForFirstBlock();
            //}
            //previousTileType = randomIndex;
            pos.z += _blockSpawnConfig.OffsetZ;
        }
    }
    private int GetRandomIndexForFirstBlock()
    {
        return Random.Range(0, _blockSpawnConfig.CityTiles.Length);
    }
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