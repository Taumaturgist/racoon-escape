using UnityEngine;

public class Environment : MonoBehaviour
{
    private EnvironmentConfig _environmentConfig;
    
    public void Awake()
    {
        _environmentConfig = GetComponent<ApplicationStartUp>().EnvironmentConfig;

        Instantiate(_environmentConfig.DirectionalLight, Vector3.zero, Quaternion.Euler(50,-30,0));
    }
}
