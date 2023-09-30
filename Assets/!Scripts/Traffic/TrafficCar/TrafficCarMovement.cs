using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Traffic
{
    public class TrafficCarMovement : MonoBehaviour
    {
        private TrafficCarState State;
        private float _speed;
        private float _slowSpeed;
        private float _rayDistance = 2.0f;
        private Rigidbody _rigidbody;
        private Vector3 _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _speed = Random.Range(5.0f, 5.5f);
            State = TrafficCarState.Driving;
            _direction = transform.forward;
        }

        private void Update()
        {
            RealseRays();
            
            switch (State)
            {
                case TrafficCarState.Driving:
                    Drive();
                    return;
                case TrafficCarState.SlowDown:
                    Stop();
                    return;
                case TrafficCarState.Stopped:
                    Stop();
                    return;
            }
        }

        private void RealseRays()
        {
            RaycastHit hit;
            
        }
        
        private void Drive()
        {
            _rigidbody.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);
        }
        
        private void SlowDown()
        {
            _rigidbody.MovePosition(transform.position + 
                                    transform.forward *
                                    Mathf.MoveTowards(_speed, _slowSpeed, Time.deltaTime));
        }
        private void Stop()
        {
            _rigidbody.MovePosition(transform.position +
                                    transform.forward *
                                    Mathf.MoveTowards(_speed, 0f, Time.deltaTime));
        }
    }
}