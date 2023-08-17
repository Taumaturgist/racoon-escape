using UnityEngine;

public class BlockStorageCreation : MonoBehaviour
{
    private BlockSpawnConfig _blockSpawnConfig;
    private BuildingSpawnConfig _buildingSpawnConfig;
    private TrafficSpawnConfig _trafficSpawnConfig;
    private BlockSpawner _blockStorage;

    private void Awake()
    {
        _blockSpawnConfig = GetComponent<ApplicationStartUp>().BlockSpawnConfig;
        _buildingSpawnConfig = GetComponent<ApplicationStartUp>().BuildingSpawnConfig;
        _trafficSpawnConfig = GetComponent<ApplicationStartUp>().TrafficSpawnConfig;

        _blockStorage = Instantiate(_blockSpawnConfig.BlockStorage);
        _blockStorage.Launch(
            _blockSpawnConfig,
            _buildingSpawnConfig,
            _trafficSpawnConfig);
    }
}