using System.Collections;
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
        if (_smallBuildingsIndexesList.Count > 0)
        {
            if (_smallBuildingsIndexesList.Contains(_buildingNumber))
            {
                var randomInt = Random.Range(0, _smallFirstFloorsList.Count);
                Instantiate(_smallFirstFloorsList[randomInt], transform.position, transform.rotation, transform);
            }
            else
            {
                var randomInt = Random.Range(0, _bigFirstFloorsList.Count);
                Instantiate(_bigFirstFloorsList[randomInt], transform.position, transform.rotation, transform);

                var angleY = _isOnLeftStreetSide == true ? 0 : 180;
                transform.Rotate(0, angleY, 0);
            }
        }        
        else
        {
            var randomInt = Random.Range(0, _bigFirstFloorsList.Count);
            Instantiate(_bigFirstFloorsList[randomInt], transform.position, transform.rotation, transform);

            var angleY = _isOnLeftStreetSide == true ? 0 : 180;
            transform.Rotate(0, angleY, 0);
        }

        
    }
}
