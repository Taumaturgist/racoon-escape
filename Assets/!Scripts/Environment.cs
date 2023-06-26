using UnityEngine;

public class Environment : MonoBehaviour
{
    private EnvironmentConfig _environmentConfig;

    private Shop _shop;
    
    public void Awake()
    {
        _environmentConfig = GetComponent<ApplicationStartUp>().EnvironmentConfig;
        _shop = GetComponent<Shop>();

        Instantiate(_environmentConfig.DirectionalLight, Vector3.zero, _environmentConfig.LightQuaternion);
        var UI = Instantiate(_environmentConfig.UIController, Vector3.zero, Quaternion.Euler(Vector3.zero));
        UI.Launch(_shop);
    }
}
