using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private List<GameObject> blocks = new();
    [SerializeField] private GameObject block;

    private void Start()
    {
        BlockCreation();
    }

    private void BlockCreation()
    {
        Vector3 _position = Vector3.zero;
        Quaternion _rotation = Quaternion.identity;

        Instantiate(block, _position, _rotation);
        block.AddComponent<Block>();

        var blockComponent = block.GetComponent<Block>();
        if (blockComponent != null)
        {
            blockComponent.blockId = SetBlockId();
            blockComponent.blockType = SetBlockType();
            blockComponent.tilesCount = SetTilesCount();
        }

        for (int tileNumber = 0; tileNumber < blockComponent.tilesCount; tileNumber++)
        {
            Vector3 offset = new Vector3(0f, 0f, tileNumber * 200f);
            TileCreation(
                blockComponent.blockId, 
                blockComponent.blockType, 
                blockComponent.tilesCount,
                offset
                );
        }

        blocks.Add(block);
    }
    private int SetBlockId()
    {
        return 0;
    }
    private int SetBlockType()
    {
        return 0;
    }
    private int SetTilesCount()
    {
        return 1;
    }

    private void TileCreation(int id, int blockType, int tilesCount, Vector3 offset)
    { 
        
    }

    private void OnTriggerEnter(Collider other)
    {
        BlockCreation();  
    }

    private void BlockDestroying(GameObject block)
    {
        Destroy(block);
    }
}

