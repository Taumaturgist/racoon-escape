using UnityEngine;

public class ApplicationStartUp : MonoBehaviour
{
    [SerializeField] private bool isDevelopMode;

    [Header("Configs")]
    public PlayerAccountConfig PlayerAccountConfig;
    public EnvironmentConfig EnvironmentConfig;
    public DebugConfig DebugConfig;
    public BlockSpawnConfig BlockSpawnConfig;
    
    private void Awake()
    {
        gameObject.AddComponent<PlayerDataStorage>();
        gameObject.AddComponent<Serializer>();
        gameObject.AddComponent<DebugSettings>();
        gameObject.AddComponent<PlayerAccount>();
        gameObject.AddComponent<Environment>();
        gameObject.AddComponent<BlockStorageCreation>();
    }
}
