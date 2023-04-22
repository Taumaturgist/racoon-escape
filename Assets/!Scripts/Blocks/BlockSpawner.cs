using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private List<GameObject> blocks = new();
    [SerializeField] private GameObject block;
    private int previousBlockType;

    private TilesList tilesList;
    [SerializeField] private GameObject tile;
    private const int minTileCountInBlock = 8;
    private const int maxTileCountInBlock = 12;

    private int offsetZ = 200;

    private void Awake()
    {
        tilesList = GetComponent<TilesList>();

        FirstTwoBlocksCreation();
        
    }

    private void FirstTwoBlocksCreation()
    {
        var _position = Vector3.zero;
        var _rotation = Quaternion.identity;

        int tileCount = 15;
        var tiles = new GameObject[tileCount];
        
        var firstBlock = Instantiate(block, _position, _rotation, transform);
        firstBlock.AddComponent<Block>();
        var firstBlockComponent = firstBlock.GetComponent<Block>();

        firstBlockComponent.blockID = SetBlockID();

        Debug.Log(firstBlockComponent.blockID);

        for (int i = 0; i < tileCount; i++)
        {
            var randomIndex = Random.Range(0, tilesList.cityTiles.Length);
            tiles[i] = Instantiate(tilesList.cityTiles[randomIndex], _position, _rotation, firstBlock.transform);
            _position.z += offsetZ;
        }
        blocks.Add(block);

        var secondBlock = Instantiate(block, _position, _rotation, transform);
        secondBlock.AddComponent<Block>();
        var secondBlockComponent = secondBlock.GetComponent<Block>();
        secondBlockComponent.blockID = SetBlockID();

        Debug.Log(secondBlockComponent.blockID);
        for (int i = 0; i < tileCount; i++)
        {
            var randomIndex = Random.Range(0, tilesList.cityTiles.Length);
            tiles[i] = Instantiate(tilesList.cityTiles[randomIndex], _position, _rotation, secondBlock.transform);
            _position.z += offsetZ;
        }
        blocks.Add(block);
    }

    private void BlockCreation()
    {
        Vector3 _position = Vector3.zero;
        Quaternion _rotation = Quaternion.identity;

        var newBlock = Instantiate(block, _position, _rotation);

        newBlock.AddComponent<Block>();
        var newBlockComponent = newBlock.GetComponent<Block>();

        newBlockComponent.blockID = SetBlockID();
        newBlockComponent.blockType = SetBlockType();
        newBlockComponent.tilesCount = SetTilesCount();

        // Создать тайлы внутри блока
        for (int tileNumber = 0; tileNumber < newBlockComponent.tilesCount; tileNumber++)
        {
            Vector3 offset = new Vector3(0f, 0f, tileNumber * offsetZ);
        }
        blocks.Add(newBlock);
    }
    private void BlockDestroying(GameObject block)
    {
        Destroy(block);
    }

    private int SetBlockID()
    {
        return blocks.Count;
    }
    private int SetBlockType()
    {
        return 0;
    }
    private int SetTilesCount()
    {
        var tileCount = Random.Range(8, 13);
        return tileCount;
    }
}