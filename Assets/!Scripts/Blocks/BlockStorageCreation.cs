using UnityEngine;

public class BlockStorageCreation : MonoBehaviour
{
    private BlockSpawnConfig _blockSpawnConfig;

    private BlockSpawner _blockStorage;

    private void Awake()
    {
        _blockSpawnConfig = GetComponent<ApplicationStartUp>().BlockSpawnConfig;
        _blockStorage = Instantiate(
                                _blockSpawnConfig.BlockSpawner,
                                transform.position,
                                transform.rotation);
        _blockStorage.Launch(_blockSpawnConfig);
    }
}