using UnityEngine;

[CreateAssetMenu(fileName = "EnvironmentConfig", menuName = "Configs/EnvironmentConfig", order = 51)]
public class EnvironmentConfig : ScriptableObject
{
    public Light DirectionalLight;
    public Quaternion LightQuaternion;

    public UIController UIController;

    public Shop Shop;
}
