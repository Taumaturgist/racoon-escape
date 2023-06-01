using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BuildingSpawnConfig", menuName = "Configs/BuildingSpawnConfig", order = 51)]
public class BuildingSpawnConfig : ScriptableObject
{
    public int MinFloorsAmount;
    public int MaxFloorsAmount;

    public List<GameObject> BigFirstFloorsList;
    public List<GameObject> SmallFirstFloorsList;

    public List<GameObject> BigMiddleFloorsList;
    public List<GameObject> SmallMiddleFloorsList;

    public List<GameObject> BigRoofList;
    public List<GameObject> SmallRoofList;
}
