using UnityEngine;

public class BlockStorageCreation : MonoBehaviour
{
    private BlockSpawnConfig _blockSpawnConfig;

    private void Awake()
    {
        _blockSpawnConfig = GetComponent<ApplicationStartUp>().BlockSpawnConfig;
        var blockStorage = Instantiate(
                                _blockSpawnConfig.BlockStorage, 
                                transform.position, 
                                transform.rotation);

        blockStorage.AddComponent<BlockStorage>();
        blockStorage.AddComponent<BlockSpawner>();
    }
}