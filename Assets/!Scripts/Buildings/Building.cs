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
        if (_smallBuildingsIndexesList.Count > 0)
        {
            if (_smallBuildingsIndexesList.Contains(_buildingNumber))
            {
                var randomInt = Random.Range(0, _smallFirstFloorsList.Count);
                Instantiate(_smallFirstFloorsList[randomInt], transform.position, transform.rotation, transform);

                switch (_smallBuildingsIndexesList.Count)
                {
                    case 2:
                        switch (_buildingNumber)
                        {
                            case 2:
                                transform.Rotate(0, 180, 0);
                                transform.localScale = new Vector3(
                                    -transform.localScale.x, 
                                    transform.localScale.y, 
                                    transform.localScale.z);
                                break;
                            case 4:
                                transform.localScale = new Vector3(
                                    -transform.localScale.x,
                                    transform.localScale.y,
                                    transform.localScale.z);
                                break;
                            case 5:
                                transform.Rotate(0, 180, 0);
                                break;
                        }
                        break;
                    case 4:
                        switch (_buildingNumber)
                        {
                            case 2:
                                transform.Rotate(0, 180, 0);
                                transform.localScale = new Vector3(
                                    -transform.localScale.x,
                                    transform.localScale.y,
                                    transform.localScale.z);
                                break;
                            case 5:
                                transform.localScale = new Vector3(
                                    -transform.localScale.x,
                                    transform.localScale.y,
                                    transform.localScale.z);
                                break;
                            case 6:
                                transform.Rotate(0, 180, 0);
                                break;
                        }
                        break;
                }
            }
            else
            {
                var randomInt = Random.Range(0, _bigFirstFloorsList.Count);
                Instantiate(_bigFirstFloorsList[randomInt], transform.position, transform.rotation, transform);

                _angleY = _isOnLeftStreetSide == true ? 0 : 180;
                transform.Rotate(0, _angleY, 0);
            }
        }        
        else
        {
            var randomInt = Random.Range(0, _bigFirstFloorsList.Count);
            Instantiate(_bigFirstFloorsList[randomInt], transform.position, transform.rotation, transform);

            _angleY = _isOnLeftStreetSide == true ? 0 : 180;
            transform.Rotate(0, _angleY, 0);
        }        
    }
}