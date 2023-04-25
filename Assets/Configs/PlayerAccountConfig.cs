using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerAccountConfig", order = 51)]
public class PlayerAccountConfig : ScriptableObject
{
    public float PlayerID;

    [Header("Cars")]
    public PlayerActiveCar PlayerActiveCar;
    public Vector3 PACSpawnPosition;

    [Header("CarSettings")]
    public float CarMotorForce;
    public float CarMass;
    public float CarMaxSpeed;    
    public float MaxSteerAngle;    
    public float RestoreDirectionSpeed;
    public float WheelDampingRate;
    public float SuspensionDistance;
    public float Spring;
    public float Damper;
    public float TargetPosition;
    public float CarBreakPower;

    [Header("TO DO")]    
    public float CarAcceleration;
    public float MaxYRotationAngle;
    public float TurnSpeed;

}
