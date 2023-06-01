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

    private List<GameObject> _bigRoofList;
    private List<GameObject> _smallRoofList;

    public delegate void Delegate();

    private Vector3 _offset;
    private int _buildingNumber;
    private bool _isOnLeftStreetSide;
    private float _angleY;
    private float _height = 5f;

    private int _randomIndex;
    private int _minFloorAmount;
    private int _maxFloorAmount;

    private bool _isBigBuilding;


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

        _bigRoofList = _buildingSpawnConfig.BigRoofList;
        _smallRoofList = _buildingSpawnConfig.SmallRoofList;

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
    private void CreateSmallFirstFloor()
    {
        _isBigBuilding = false;

        var randomInt = Random.Range(0, _smallFirstFloorsList.Count);
        Instantiate(_smallFirstFloorsList[randomInt], transform.position, transform.rotation, transform);
        _randomIndex = randomInt;
    }
    private void CreateBigFirstFloor()
    {
        _isBigBuilding = true;

        var randomInt = Random.Range(0, _bigFirstFloorsList.Count);
        Instantiate(_bigFirstFloorsList[randomInt], transform.position, transform.rotation, transform);
        _randomIndex = randomInt;
    }

    private void CreateMiddleFloors(bool isBigBuilding)
    {
        var randomFloorAmount = Random.Range(_minFloorAmount, _maxFloorAmount + 1);
        

        for (int i = 0; i < randomFloorAmount; i++)
        {
            _offset = new Vector3(0f, (i + 1) * _height, 0f);
            if (isBigBuilding)
                Instantiate(_bigMiddleFloorsList[_randomIndex], transform.position + _offset, transform.rotation, transform);
            else
                Instantiate(_smallMiddleFloorsList[_randomIndex], transform.position + _offset, transform.rotation, transform);
        }
        _offset += new Vector3(0f, _height, 0f);
    }

    private void CreateRoof(bool isBigBuilding)
    {
        if (isBigBuilding)
            Instantiate(_bigRoofList[_randomIndex], transform.position + _offset, transform.rotation, transform);
        else
            Instantiate(_smallRoofList[_randomIndex], transform.position + _offset, transform.rotation, transform);
    }

    private void SetTransform()
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