using UnityEngine;

public class ApplicationStartUp : MonoBehaviour
{
    [SerializeField] private bool isDevelopMode;

    [Header("Configs")]
    public PlayerAccountConfig PlayerAccountConfig;
    public EnvironmentConfig EnvironmentConfig;
    
    private void Awake()
    {
        gameObject.AddComponent<PlayerAccount>();
        gameObject.AddComponent<Environment>();    
    }
}
