using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public int tileType;
    public Tile tile;

    private void Awake()
    {
        tile = GetComponent<Tile>();
    }
}