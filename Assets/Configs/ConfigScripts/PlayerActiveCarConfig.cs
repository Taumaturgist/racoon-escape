using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerActiveCarConfig", order = 51)]
public class PlayerActiveCarConfig : ScriptableObject
{
    [Header("GeneralCarSettings")]
    public float CarMotorForce;
    public float CarMass;
    public float CarAngularDrug;
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

    [Header("Misc")]
    public float DefeatActivationSpeed;
    public float DefeatWarningSpeed;
    public float DefeatConditionSpeed;    
}
