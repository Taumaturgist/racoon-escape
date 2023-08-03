using UnityEngine;

public class TrafficCarsSpawner : MonoBehaviour
{
    [SerializeField] private TrafficCar trafficCarPrefab;
    [SerializeField] private Vector3[] trafficCarsSpawnPoints;

    private TrafficSpawnConfig _trafficSpawnConfig;

    public void Launch(TrafficSpawnConfig trafficSpawnConfig)
    {
        _trafficSpawnConfig = trafficSpawnConfig;

        for (int i = 0; i < trafficCarsSpawnPoints.Length; i++)
        {
            TrafficCar trafficCar = Instantiate(trafficCarPrefab, transform.position + trafficCarsSpawnPoints[i], Quaternion.identity, transform);

            trafficCar.Launch(_trafficSpawnConfig);
        }
    }
}
