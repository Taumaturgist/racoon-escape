using UnityEngine;

public class Environment : MonoBehaviour
{
    private EnvironmentConfig _environmentConfig;
    
    public void Awake()
    {
        _environmentConfig = GetComponent<ApplicationStartUp>().EnvironmentConfig;

        Instantiate(_environmentConfig.DirectionalLight, Vector3.zero, _environmentConfig.LightQuaternion);
        Instantiate(_environmentConfig.UIMainPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
    }
}
