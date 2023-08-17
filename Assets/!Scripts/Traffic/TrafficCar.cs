using UnityEngine;

public class TrafficCar : MonoBehaviour
{
    private TrafficSpawnConfig _trafficSpawnConfig;
    private float _speed;

    public void Launch(TrafficSpawnConfig trafficSpawnConfig, bool isOnLeftStreetSide)
    {
        _trafficSpawnConfig = trafficSpawnConfig;
        _speed = _trafficSpawnConfig.TrafficCarsSpeed;

        if (isOnLeftStreetSide)
        {
            transform.Rotate(0f, -180f, 0f);
        }
        
        SpawnTrafficCar();
        
        TrafficCarBehavior();
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

    private void TrafficCarBehavior()
    {
                
    }
}