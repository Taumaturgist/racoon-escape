using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStorageInit : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddComponent<BlockSpawner>();
        gameObject.AddComponent<Block>();
        //gameObject.AddComponent<TileSpawner>();
        //gameObject.AddComponent<Tile>();
    }
}
