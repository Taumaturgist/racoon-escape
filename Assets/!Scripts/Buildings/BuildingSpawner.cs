using UnityEngine;
using System.Collections.Generic;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private Vector3[] buildingSpawnPoints;
    [SerializeField] private BuildingSpawnConfig buildingSpawnConfig; //set as private and put config to AppStartUp
    [SerializeField] private Building buildingPrefab;    

    [SerializeField] private List<int> smallBuildingIndexes;

    private bool _isOnLeftStreetSide;

    private void Awake()
    {
        for (int i = 0; i < buildingSpawnPoints.Length; i++)
        {
            Building building = Instantiate(buildingPrefab, buildingSpawnPoints[i], Quaternion.identity, transform);
            building.Launch(buildingSpawnConfig, smallBuildingIndexes, i, _isOnLeftStreetSide);
        }
    }

    
}
