using UnityEngine;

public class BlockStorageCreation : MonoBehaviour
{
    private BlockSpawnConfig _blockSpawnConfig;
    private BlockSpawner _blockStorage;
    private BuildingSpawnConfig _buildingSpawnConfig;

    private void Awake()
    {
        _blockSpawnConfig = GetComponent<ApplicationStartUp>().BlockSpawnConfig;
        _buildingSpawnConfig = GetComponent<ApplicationStartUp>().BuildingSpawnConfig;

        _blockStorage = Instantiate(_blockSpawnConfig.BlockStorage);
        _blockStorage.Launch(_blockSpawnConfig, _buildingSpawnConfig);
    }
}