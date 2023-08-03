using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class TrafficCar : MonoBehaviour
{
    private TrafficSpawnConfig _trafficSpawnConfig;
    private float _speed;

    public void Launch(TrafficSpawnConfig trafficSpawnConfig)
    {
        _trafficSpawnConfig = trafficSpawnConfig;
        _speed = _trafficSpawnConfig.TrafficCarsSpeed;

        SpawnTrafficCar();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.World);
    }

    private void SpawnTrafficCar()
    {
        var randomInt = Random.Range(0, _trafficSpawnConfig.TrafficCars.Count);
        Instantiate(_trafficSpawnConfig.TrafficCars[randomInt], transform.position, transform.rotation, transform);
    }
}