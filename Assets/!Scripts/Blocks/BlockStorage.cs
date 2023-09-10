using Traffic;
using UnityEngine;

public class BlockStorage : MonoBehaviour
{
    private BlockSpawnConfig _blockSpawnConfig;
    private BuildingSpawnConfig _buildingSpawnConfig;
    private TrafficConfig _trafficConfig;
    private BlockSpawner _blockStorage;

    private void Awake()
    {
        _blockSpawnConfig = GetComponent<ApplicationStartUp>().BlockSpawnConfig;
        _buildingSpawnConfig = GetComponent<ApplicationStartUp>().BuildingSpawnConfig;
        _trafficConfig = GetComponent<ApplicationStartUp>().trafficConfig;

        _blockStorage = Instantiate(_blockSpawnConfig.BlockStorage);
        _blockStorage.Launch(
            _blockSpawnConfig,
            _buildingSpawnConfig,
            _trafficConfig);
    }
}