using Traffic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private BuildingSpawner _buildingSpawner;
    private TrafficController _trafficController;

    public void Launch(BuildingSpawnConfig buildingSpawnConfig, TrafficConfig trafficConfig, eBlockType blockType)
    {
        _buildingSpawner = GetComponent<BuildingSpawner>();

        if (_buildingSpawner != null)
        {
            _buildingSpawner.Launch(buildingSpawnConfig);
        }

        _trafficController = new TrafficController(trafficConfig, blockType, this);
    }
}