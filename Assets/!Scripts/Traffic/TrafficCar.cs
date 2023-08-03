using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class TrafficCar : MonoBehaviour
{
    private TrafficSpawnConfig _trafficSpawnConfig;
    private float _speed;
    private bool _isOnLeftStreetSide;

    public void Launch(TrafficSpawnConfig trafficSpawnConfig, bool isOnLeftStreetSide)
    {
        _trafficSpawnConfig = trafficSpawnConfig;
        _speed = _trafficSpawnConfig.TrafficCarsSpeed;
        _isOnLeftStreetSide = isOnLeftStreetSide;

        if (isOnLeftStreetSide)
        {
            transform.Rotate(0f, -180f, 0f);
        }

        SpawnTrafficCar();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.Self);
    }

    private void SpawnTrafficCar()
    {
        var randomInt = Random.Range(0, _trafficSpawnConfig.TrafficCars.Count);
        Instantiate(_trafficSpawnConfig.TrafficCars[randomInt], transform.position, transform.rotation, transform);
    }
}