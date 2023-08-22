using Traffic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private BuildingSpawner _buildingSpawner;
    private TrafficCarsSpawner _trafficCarsSpawner;
    private BuildingSpawnConfig _buildingSpawnConfig;
    private TrafficConfig _trafficConfig;
    public void Launch() { }

    public void Launch(BuildingSpawnConfig buildingSpawnConfig, TrafficConfig trafficConfig)
    {
        _buildingSpawnConfig = buildingSpawnConfig;
        _trafficConfig = trafficConfig;

        _buildingSpawner = GetComponent<BuildingSpawner>();
        _trafficCarsSpawner = GetComponent<TrafficCarsSpawner>();

        if (_buildingSpawner != null)
            _buildingSpawner.Launch(_buildingSpawnConfig);

        new TrafficController(_trafficConfig);

        // if (_trafficCarsSpawner != null)
        //     _trafficCarsSpawner.Launch(_trafficSpawnConfig);
    }
}