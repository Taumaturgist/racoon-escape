using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerAccountConfig", order = 51)]
public class PlayerAccountConfig : ScriptableObject
{
    public float PlayerID;

    [Header("Camera")]
    public CameraSettings Camera;

    [Header("Cars")]
    public PlayerActiveCar PlayerActiveCar;
    public Vector3 PACSpawnPosition;

    [Header("GeneralCarSettings")]
    public float CarMotorForce;
    public float CarMass;
    public float CarMassCenterShiftY;
    public float CarMaxSpeed;    
    public float MaxSteerAngle;
    public float LimitRotationY;    
    public float CarBreakPower;

    [Header("CarSuspensionSettings")]
    public float WheelDampingRate;
    public float SuspensionDistance;
    public float Spring;
    public float Damper;
    public float TargetPosition;
    
    [Header("CarDriveSettings")]
    public bool FrontWheelDrive;
    public bool RearWheelDrive;

    [Header("Nitro")]
    public float NitroCapacity;
    public float NitroUsageSpeed;
    public float NitroRestorationSpeed;
    public float NitroSpeedBoost;
    public float NitroTorqueBoost;
    
}
