using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CameraSettingsConfig", menuName = "Configs/CameraSettingsConfig", order = 51)]
public class CameraSettingsConfig : ScriptableObject
{
    [Serializable]
    public struct CameraTransformData
    {
        public eCarModel carModel;
        public Vector3 offsetShoulderView;
        public Quaternion rotationShoulderView;
    }

    [Serializable]
    public struct CameraShopData
    {
        public Vector3 position;
        public Vector3 rotation;
    }

    [SerializeField] public CameraTransformData[] CameraTransformDataSet;

    public CameraShopData CamShopData;

    public float CamShopSwitchSpeed;
}