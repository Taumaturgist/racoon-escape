using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private Vector3[] buildingSpawnPoints;
    [SerializeField] private BuildingSpawnConfig buildingSpawnConfig; //set as private and put config to AppStartUp
    [SerializeField] private Building buildingPrefab;

    private GameObject[] _bigFirstFloors;
    private GameObject[] _smallFirstFloors;
}
