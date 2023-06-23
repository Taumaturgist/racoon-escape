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
}
