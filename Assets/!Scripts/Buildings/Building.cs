using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingSpawnConfig _buildingSpawnConfig;

    private List<GameObject> _bigFirstFloorsList;
    private List<GameObject> _smallFirstFloorsList;

    private List<int> _smallBuildingsIndexesList;
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
    }

    private void CreateFirstFloor()
    {
        var smallBuildingsExists = _smallBuildingsIndexesList.Count > 0;

        if (smallBuildingsExists)
        {
            var currentBuildingIsSmall = _smallBuildingsIndexesList.Contains(_buildingNumber);

            if (currentBuildingIsSmall)
                CreateSmallFirstFloor();
            else
                CreateBigFirstFloor();
        }
        else
            CreateBigFirstFloor();
    }

    private void CreateSmallFirstFloor()
    {
        var randomInt = Random.Range(0, _smallFirstFloorsList.Count);
        Instantiate(_smallFirstFloorsList[randomInt], transform.position, transform.rotation, transform);

        int[] indexes;

        switch (_smallBuildingsIndexesList.Count)
        {
            case 2:
                indexes = new int[] { 2, 4, 5 };
                SetTransformForSmallBuilding(indexes);
                break;
            case 4:
                indexes = new int[] { 2, 5, 6 };
                SetTransformForSmallBuilding(indexes);
                break;
        }
    }
    private void SetTransformForSmallBuilding(int[] indexes)
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

    private void CreateBigFirstFloor()
    {
        var randomInt = Random.Range(0, _bigFirstFloorsList.Count);
        Instantiate(_bigFirstFloorsList[randomInt], transform.position, transform.rotation, transform);
        SetTransformForBigBuilding();
    }
    private void SetTransformForBigBuilding()
    {
        _angleY = _isOnLeftStreetSide == true ? 0 : 180;
        transform.Rotate(0, _angleY, 0);
    }
}