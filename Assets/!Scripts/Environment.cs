using UnityEngine;

public class Environment : MonoBehaviour
{
    private EnvironmentConfig _environmentConfig;
    
    public void Awake()
    {
        _environmentConfig = GetComponent<ApplicationStartUp>().EnvironmentConfig;
        var shopConfig = GetComponent<ApplicationStartUp>().ShopCarModelsConfig;
        var playerAccountConfig = GetComponent<ApplicationStartUp>().PlayerAccountConfig;

        var light = Instantiate(_environmentConfig.DirectionalLight, Vector3.zero, _environmentConfig.LightQuaternion);

        var shop = Instantiate(_environmentConfig.Shop, Vector3.zero, Quaternion.Euler(Vector3.zero));
        shop.Launch(shopConfig, playerAccountConfig);

        var UI = Instantiate(_environmentConfig.UIController, Vector3.zero, Quaternion.Euler(Vector3.zero));

        UI.Launch(shop);
    }
}
