using UnityEngine;

public class TrafficCarsSpawner : MonoBehaviour
{
    [SerializeField] private TrafficCar trafficCarPrefab;
    [SerializeField] private Vector3[] trafficCarsSpawnPoints;

    private TrafficSpawnConfig _trafficSpawnConfig;
    private bool _isOnLeftStreetSide;

    public void Launch(TrafficSpawnConfig trafficSpawnConfig)
    {
        _trafficSpawnConfig = trafficSpawnConfig;

        for (int i = 0; i < trafficCarsSpawnPoints.Length; i++)
        {
            var trafficCar = Instantiate(trafficCarPrefab, transform.position + trafficCarsSpawnPoints[i], Quaternion.identity, transform);
            _isOnLeftStreetSide = trafficCarsSpawnPoints[i].x < 0;

            trafficCar.Launch(_trafficSpawnConfig, _isOnLeftStreetSide);
        }
    }
}