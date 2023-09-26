using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BuildingSpawnConfig", menuName = "Configs/BuildingSpawnConfig", order = 51)]
public class BuildingSpawnConfig : ScriptableObject
{
    public int MinFloorsAmount;
    public int MaxFloorsAmount;

    public GameObject Bank;

    [Header("FirstFloors")]
    public List<GameObject> BigFirstFloorsList;
    public List<GameObject> SmallFirstFloorsList;

    [Header("MiddleFloors")]
    public List<GameObject> BigMiddleFloorsList;
    public List<GameObject> SmallMiddleFloorsList;

    [Header("LastFloors")]
    public GameObject BigLastFloorForBuildNumber3;
    public GameObject BigLastFloorForBuildNumber4;

    public GameObject SmallLastFloorForBuildNumber3;
    public GameObject SmallLastFloorForBuildNumber4;

    [Header("Roofs")]
    public List<GameObject> BigRoofList;
    public List<GameObject> SmallRoofList;
}
