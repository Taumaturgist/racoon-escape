using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerAccountConfig", order = 51)]
public class PlayerAccountConfig : ScriptableObject
{
    public float PlayerID;

    [Header("Cars")]
    public PlayerActiveCar PlayerActiveCar;
    public Vector3 PACSpawnPosition;

    [Header("GeneralCarSettings")]
    public float CarMotorForce;
    public float CarMass;
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

    [Header("TO DO")]
    public float NitroCapacity;
    public float NitroRestorationSpeed;
    public float NitroSpeedCapRaise;
    public float NitroAcceleration; //add torque?

    //not used
    //public float RestoreDirectionSpeed;

}
