using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CameraSettingsConfig", menuName = "Configs/CameraSettingsConfig", order = 51)]
public class CameraSettingsConfig : ScriptableObject
{
    [Serializable]
    public struct CameraTransformData
    {
        public int carID;
        public string carName;
        public Vector3 offsetShoulderView;
        public Quaternion rotationShoulderView;
    }

    [SerializeField]
    public CameraTransformData[] CameraTransformDataSet;
}