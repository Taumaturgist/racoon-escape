/*
 *  It is necessary to separate the logic of creating a middle floor and a roof for large and small buildings
 */

using UnityEngine;
using System.Collections.Generic;

public class Building : MonoBehaviour
{
    private BuildingSpawnConfig _buildingSpawnConfig;
    private List<GameObject> _bigFirstFloorsList;
    private List<GameObject> _smallFirstFloorsList;
    private List<int> _smallBuildingsIndexesList;

    private List<GameObject> _bigMiddleFloorsList;
    private List<GameObject> _smallMiddleFloorsList;

    private GameObject _bigLastFloorForBuildNumber3;
    private GameObject _bigLastFloorForBuildNumber4;

    private GameObject _smallLastFloorForBuildNumber3;
    private GameObject _smallLastFloorForBuildNumber4;

    private List<GameObject> _bigRoofList;
    private List<GameObject> _smallRoofList;

    public delegate void Delegate();

    private Vector3 _heightMiddleFloor;
    private int _buildingNumber;
    private bool _isOnLeftStreetSide;
    private float _angleY;
    private readonly float _height = 5f;
    private Vector3 _offset;

    private int _randomIndex;
    private int _minFloorAmount;
    private int _maxFloorAmount;

    private bool _isBigBuilding;

    private const int _indexBuildNumber3 = 2;
    private const int _indexBuildNumber4 = 3;


    public void Launch(BuildingSpawnConfig buildingSpawnConfig, List<int> smallBuildingsIndexes, int buildingNumber, bool isOnLeftStreetSide)
    {
        _buildingSpawnConfig = buildingSpawnConfig;
        _smallBuildingsIndexesList = smallBuildingsIndexes;
        _buildingNumber = buildingNumber;
        _isOnLeftStreetSide = isOnLeftStreetSide;
        _minFloorAmount = _buildingSpawnConfig.MinFloorsAmount;
        _maxFloorAmount = _buildingSpawnConfig.MaxFloorsAmount;

        _bigFirstFloorsList = _buildingSpawnConfig.BigFirstFloorsList;
        _smallFirstFloorsList = _buildingSpawnConfig.SmallFirstFloorsList;

        _bigMiddleFloorsList = _buildingSpawnConfig.BigMiddleFloorsList;
        _smallMiddleFloorsList = _buildingSpawnConfig.SmallMiddleFloorsList;

        _bigLastFloorForBuildNumber3 = _buildingSpawnConfig.BigLastFloorForBuildNumber3;
        _bigLastFloorForBuildNumber4 = _buildingSpawnConfig.BigLastFloorForBuildNumber4;
        _smallLastFloorForBuildNumber3 = _buildingSpawnConfig.SmallLastFloorForBuildNumber3;
        _smallLastFloorForBuildNumber4 = _buildingSpawnConfig.SmallLastFloorForBuildNumber4;

        _bigRoofList = _buildingSpawnConfig.BigRoofList;
        _smallRoofList = _buildingSpawnConfig.SmallRoofList;

        _heightMiddleFloor = Vector3.zero;
        _offset = new Vector3(0f, _height, 0f);

        CreateBuilding();
    }

    private void CreateBuilding()
    {
        CreateFirstFloor();
        CreateMiddleFloors(_isBigBuilding);
        CreateRoof(_isBigBuilding);

        SetTransform();
    }
    private void CreateFirstFloor()
    {
        Delegate createSmallFirstFloor = CreateSmallFirstFloor;
        Delegate createBigFirstFloor = CreateBigFirstFloor;

        ActionDependingBuildingSize(createSmallFirstFloor, createBigFirstFloor);
    }
    private void CreateBigFirstFloor()
    {
        _isBigBuilding = true;

        var randomInt = Random.Range(0, _bigFirstFloorsList.Count);
        Instantiate(_bigFirstFloorsList[randomInt], transform.position, transform.rotation, transform);

        _randomIndex = randomInt;
    }
    private void CreateSmallFirstFloor()
    {
        _isBigBuilding = false;

        var randomInt = Random.Range(0, _smallFirstFloorsList.Count);
        Instantiate(_smallFirstFloorsList[randomInt], transform.position, transform.rotation, transform);

        _randomIndex = randomInt;
    }

    private void CreateMiddleFloors(bool isBigBuilding)
    {
        var randomFloorAmount = UnityEngine.Random.Range(_minFloorAmount, _maxFloorAmount + 1);

        if (_randomIndex == _indexBuildNumber3 || _randomIndex == _indexBuildNumber4)
            randomFloorAmount -= 1;

        for (int i = 0; i < randomFloorAmount; i++)
        {
            _heightMiddleFloor += _offset;
            if (isBigBuilding)
                CreateMiddleFloor(_bigMiddleFloorsList, _randomIndex);
            else
                CreateMiddleFloor(_smallMiddleFloorsList, _randomIndex);
        }

        _heightMiddleFloor += _offset;

        if (isBigBuilding)
        {
            if (_randomIndex == _indexBuildNumber3)
            {
                Instantiate(_bigLastFloorForBuildNumber3, transform.position + _heightMiddleFloor, transform.rotation, transform);
                _heightMiddleFloor += _offset;
            }

            if (_randomIndex == _indexBuildNumber4)
            {
                Instantiate(_bigLastFloorForBuildNumber4, transform.position + _heightMiddleFloor, transform.rotation, transform);
                _heightMiddleFloor += _offset;
            }
        }
        else
        {
            if (_randomIndex == _indexBuildNumber3)
            {
                Instantiate(_smallLastFloorForBuildNumber3, transform.position + _heightMiddleFloor, transform.rotation, transform);
                _heightMiddleFloor += _offset;
            }

            if (_randomIndex == _indexBuildNumber4)
            {
                Instantiate(_smallLastFloorForBuildNumber4, transform.position + _heightMiddleFloor, transform.rotation, transform);
                _heightMiddleFloor += _offset;
            }
        }
    }

    private void CreateMiddleFloor(List<GameObject> middleFloors, int index)
    {
        Instantiate(middleFloors[index], transform.position + _heightMiddleFloor, transform.rotation, transform);
    }

    private void CreateRoof(bool isBigBuilding)
    {
        if (isBigBuilding)
            Instantiate(_bigRoofList[_randomIndex], transform.position + _heightMiddleFloor, transform.rotation, transform);
        else
            Instantiate(_smallRoofList[_randomIndex], transform.position + _heightMiddleFloor, transform.rotation, transform);
    }

    private void SetTransform()
    {
        Delegate actionBasedOnSmallBuildingsIndexesCount = ActionBasedOnSmallBuildingsIndexesCount;
        Delegate transformBigBuilding = TransformBigBuilding;

        ActionDependingBuildingSize(actionBasedOnSmallBuildingsIndexesCount, transformBigBuilding);
    }
    private void ActionBasedOnSmallBuildingsIndexesCount()
    {
        int[] pointsIndexes;

        switch (_smallBuildingsIndexesList.Count)
        {
            case 2:
                pointsIndexes = new int[] { 2, 4, 5 };
                TransformSmallBuilding(pointsIndexes);
                break;
            case 4:
                pointsIndexes = new int[] { 2, 5, 6 };
                TransformSmallBuilding(pointsIndexes);
                break;
        }
    }
    private void TransformSmallBuilding(int[] pointsIndexes)
    {
        if (_buildingNumber == pointsIndexes[0])
        {
            transform.Rotate(0, 180, 0);
            transform.localScale = new Vector3(
                -transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z);
        }
        if (_buildingNumber == pointsIndexes[1])
        {
            transform.localScale = new Vector3(
                -transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z);
        }
        if (_buildingNumber == pointsIndexes[2])
            transform.Rotate(0, 180, 0);
    }
    private void TransformBigBuilding()
    {
        _angleY = _isOnLeftStreetSide ? 0 : 180;
        transform.Rotate(0, _angleY, 0);
    }
    private void ActionDependingBuildingSize(Delegate action1, Delegate action2)
    {
        if (_smallBuildingsIndexesList.Count > 0)
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
}