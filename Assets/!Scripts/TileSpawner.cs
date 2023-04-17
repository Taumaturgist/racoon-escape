using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public int tileType;
    public Vector3 tilePosition;
    private void tileCreation()
    {
        Instantiate(gameObject, tilePosition, Quaternion.identity);
    }
}
