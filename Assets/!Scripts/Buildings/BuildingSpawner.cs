using UnityEngine;
using System.Collections.Generic;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private Vector3[] buildingSpawnPoints;
    [SerializeField] private BuildingSpawnConfig buildingSpawnConfig; //set as private and put config to AppStartUp
    [SerializeField] private Building buildingPrefab;

    [SerializeField] private int[] smallBuildingIndexes;

    private void Awake()
    {
        for (int i = 0; i < buildingSpawnPoints.Length; i++)
        {
            Instantiate(buildingPrefab, transform.position, Quaternion.identity);
        }
    }

    private GameObject[] _bigFirstFloors;
    private GameObject[] _smallFirstFloors;
}
