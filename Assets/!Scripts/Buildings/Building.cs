using JetBrains.Annotations;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingSpawnConfig _buildingSpawnConfig;

    private List<GameObject> _bigFirstFloorsList;
    private List<GameObject> _smallFirstFloorsList;
    private List<int> _smallBuildingsIndexesList;

    public delegate void Delegate();

    private int _buildingNumber;
    private bool _isOnLeftStreetSide;
    private float _angleY;

    public void Launch(BuildingSpawnConfig buildingSpawnConfig, List<int> smallBuildingsIndexes, int buildingNumber, bool isOnLeftStreetSide)
    {
        _buildingSpawnConfig = buildingSpawnConfig;
        _smallBuildingsIndexesList = smallBuildingsIndexes;
        _buildingNumber = buildingNumber;
        _isOnLeftStreetSide = isOnLeftStreetSide;

        _bigFirstFloorsList = buildingSpawnConfig.BigFirstFloorsList;
        _smallFirstFloorsList = buildingSpawnConfig.SmallFirstFloorsList;

        CreateBuilding();
    }

    private void CreateBuilding()
    {
        CreateFirstFloor();
        SetTransformForBuilding();
    }

    private void ActionDependingBuildingSize(Delegate action1, Delegate action2)
    {
        var smallBuildingsExists = _smallBuildingsIndexesList.Count > 0;

        if (smallBuildingsExists)
        {
            var currentBuildingIsSmall = _smallBuildingsIndexesList.Contains(_buildingNumber);

            if (currentBuildingIsSmall)
                action1.Invoke();
            else
                action2.Invoke();
        }
        else
            action2.Invoke();
    }

    private void CreateFirstFloor()
    {
        Delegate createSmallFirstFloor = CreateSmallFirstFloor;
        Delegate createBigFirstFloor = CreateBigFirstFloor;

        ActionDependingBuildingSize(createSmallFirstFloor, createBigFirstFloor);
    }
    private void CreateSmallFirstFloor()
    {
        var randomInt = Random.Range(0, _smallFirstFloorsList.Count);
        Instantiate(_smallFirstFloorsList[randomInt], transform.position, transform.rotation, transform);
    }
    private void CreateBigFirstFloor()
    {
        var randomInt = Random.Range(0, _bigFirstFloorsList.Count);
        Instantiate(_bigFirstFloorsList[randomInt], transform.position, transform.rotation, transform);
    }
    private void SetTransformForBuilding()
    {
        Delegate actionBasedOnSmallBuildingsIndexesCount = ActionBasedOnSmallBuildingsIndexesCount;
        Delegate transformBigBuilding = TransformBigBuilding;

        ActionDependingBuildingSize(actionBasedOnSmallBuildingsIndexesCount, transformBigBuilding);
    }
    private void ActionBasedOnSmallBuildingsIndexesCount()
    {
        int[] indexes;

        switch (_smallBuildingsIndexesList.Count)
        {
            case 2:
                indexes = new int[] { 2, 4, 5 };
                TransformSmallBuilding(indexes);
                break;
            case 4:
                indexes = new int[] { 2, 5, 6 };
                TransformSmallBuilding(indexes);
                break;
        }
    }
    private void TransformSmallBuilding(int[] indexes)
    {
        if (_buildingNumber == indexes[0])
        {
            transform.Rotate(0, 180, 0);
            transform.localScale = new Vector3(
                -transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z);
        }
        if (_buildingNumber == indexes[1])
        {
            transform.localScale = new Vector3(
                -transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z);
        }
        if (_buildingNumber == indexes[2])
            transform.Rotate(0, 180, 0);
    }
    private void TransformBigBuilding()
    {
        _angleY = _isOnLeftStreetSide == true ? 0 : 180;
        transform.Rotate(0, _angleY, 0);
    }
}