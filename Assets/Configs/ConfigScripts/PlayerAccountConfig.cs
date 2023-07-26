using System;
using System.Collections.Generic;
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

    public bool IsCheatMode;
    public int CheatBalance;
}

[Serializable]
public class CarsAssortment
{
    public eCarLevel CamaroLevel;
    public eCarLevel TundraLevel;
    public eCarLevel SkylineLevel;
    public eCarLevel ViperLevel;
    public eCarLevel BenzLevel;
    public eCarLevel HurricanLevel;
}
