using UnityEngine;

public class Tile : MonoBehaviour
{
    private BuildingSpawner _buildingSpawner;
    private BuildingSpawnConfig _buildingSpawnConfig;
    public void Launch() { }

    public void Launch(BuildingSpawnConfig buildingSpawnConfig)
    {
        _buildingSpawnConfig = buildingSpawnConfig;
        _buildingSpawner = GetComponent<BuildingSpawner>();

        if (_buildingSpawner != null )
            _buildingSpawner.Launch(_buildingSpawnConfig);
    }
}