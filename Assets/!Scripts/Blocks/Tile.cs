using UnityEngine;

public class Tile : MonoBehaviour
{
    private BuildingSpawner _buildingSpawner;
    private TrafficCarsSpawner _trafficCarsSpawner;
    private BuildingSpawnConfig _buildingSpawnConfig;
    private TrafficSpawnConfig _trafficSpawnConfig;
    public void Launch() { }

    public void Launch(BuildingSpawnConfig buildingSpawnConfig, TrafficSpawnConfig trafficSpawnConfig)
    {
        _buildingSpawnConfig = buildingSpawnConfig;
        _trafficSpawnConfig = trafficSpawnConfig;

        _buildingSpawner = GetComponent<BuildingSpawner>();
        _trafficCarsSpawner = GetComponent<TrafficCarsSpawner>();


        if (_buildingSpawner != null)
            _buildingSpawner.Launch(_buildingSpawnConfig);
        
        if (_trafficCarsSpawner != null)
            _trafficCarsSpawner.Launch(_trafficSpawnConfig);
    }
}