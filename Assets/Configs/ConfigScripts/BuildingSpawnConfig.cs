using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BuildingSpawnConfig", menuName = "Configs/BuildingSpawnConfig", order = 51)]
public class BuildingSpawnConfig : ScriptableObject
{
    public List<GameObject> BigFirstFloorsList;
    public List<GameObject> SmallFirstFloorsList;

    public int MinFloorsAmount;
    public int MaxFloorsAmount;
}
