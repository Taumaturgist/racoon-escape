using UnityEngine;
using System.Collections.Generic;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private Vector3[] buildingSpawnPoints;
    [SerializeField] private Building buildingPrefab;
    [SerializeField] private List<int> smallBuildingIndexes;

    private BuildingSpawnConfig _buildingSpawnConfig;
    
    private bool _isOnLeftStreetSide;

    public void Launch(BuildingSpawnConfig buildingSpawnConfig)
    {
        _buildingSpawnConfig = buildingSpawnConfig;

        for (int i = 0; i < buildingSpawnPoints.Length; i++)
        {
            Building building = Instantiate(buildingPrefab, transform.position + buildingSpawnPoints[i], Quaternion.identity, transform);
            
            _isOnLeftStreetSide = buildingSpawnPoints[i].x < 0;

            building.Launch(_buildingSpawnConfig, smallBuildingIndexes, i, _isOnLeftStreetSide);
        }
    }
}