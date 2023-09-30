using System;
using Traffic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private BuildingSpawner _buildingSpawner;
    private TrafficCarSpawner _trafficCarSpawner;

    public void Launch(BuildingSpawnConfig buildingSpawnConfig, TrafficConfig trafficConfig, eBlockType blockType)
    {
        _buildingSpawner = GetComponent<BuildingSpawner>();

        if (_buildingSpawner != null)
        {
            _buildingSpawner.Launch(buildingSpawnConfig);
        }
        
        _trafficCarSpawner = GetComponent<TrafficCarSpawner>();
        
        if (trafficConfig != null)
        {
            _trafficCarSpawner?.Launch(trafficConfig, blockType);
        }
    }
    
}