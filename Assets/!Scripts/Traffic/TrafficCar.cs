using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrafficCar : MonoBehaviour
{
    private TrafficSpawnConfig _trafficSpawnConfig;

    private List<Vector3> _path = new();
    
    private float _offset = 10f;
    
    private float _speed;

    private int _lastIndex = 0;
    private int _direction;

    private bool _isOnLeftStreetSide;
    

    public void Launch(TrafficSpawnConfig trafficSpawnConfig, bool isOnLeftStreetSide)
    {
        _trafficSpawnConfig = trafficSpawnConfig;
        _isOnLeftStreetSide = isOnLeftStreetSide;
        _speed = _trafficSpawnConfig.TrafficCarsSpeed;
        
        _path.Add(transform.position + new Vector3(0f, 0f, _offset));
        _lastIndex = _path.Count;


        if (_isOnLeftStreetSide)
        {
            _direction = -1;
            transform.Rotate(0f, -180f, 0f);
        }
        else
            _direction = 1;
        
        SpawnTrafficCar();
    }

    private void FixedUpdate()
    {
        _path.Add(transform.position + new Vector3(0f, 0f, _offset));
        _path.RemoveAt(_lastIndex);
        _lastIndex = _path.Count;

        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 15f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
        }
        
        transform.Translate(_direction * transform.TransformDirection(Vector3.forward) * _speed * Time.deltaTime);
        
        if (transform.position.y < -50f)
            Destroy(gameObject);
    }

    private void SpawnTrafficCar()
    {
        var randomInt = Random.Range(0, _trafficSpawnConfig.TrafficCars.Count);
        Instantiate(_trafficSpawnConfig.TrafficCars[randomInt], transform.position, transform.rotation, transform);
    }
}