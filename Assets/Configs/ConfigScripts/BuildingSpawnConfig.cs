using UnityEngine;

[CreateAssetMenu(fileName = "BuildingSpawnConfig", menuName = "Configs/BuildingSpawnConfig", order = 51)]
public class BuildingSpawnConfig : ScriptableObject
{
    public GameObject[] BigFirstFloors;
    public GameObject[] SmallFloors;

    public int MinFloorsAmount;
    public int MaxFloorsAmount;
}
