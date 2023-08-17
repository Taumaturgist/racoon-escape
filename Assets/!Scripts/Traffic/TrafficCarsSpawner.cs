using UnityEngine;

public class TrafficCarsSpawner : MonoBehaviour
{
    private TrafficCar _trafficCarPrefab;
    [SerializeField] private Vector3[] trafficCarsSpawnPoints;

    private TrafficSpawnConfig _trafficSpawnConfig;
    private bool _isOnLeftStreetSide;

    public void Launch(TrafficSpawnConfig trafficSpawnConfig)
    {
        _trafficSpawnConfig = trafficSpawnConfig;
        _trafficCarPrefab = _trafficSpawnConfig.TrafficCarPrefab;

        for (int i = 0; i < trafficCarsSpawnPoints.Length; i++)
        {
            var trafficCar = Instantiate(_trafficCarPrefab, transform.position + trafficCarsSpawnPoints[i], Quaternion.identity, transform);
            _isOnLeftStreetSide = trafficCarsSpawnPoints[i].x < 0;

            trafficCar.Launch(_trafficSpawnConfig, _isOnLeftStreetSide);
        }
    }
}