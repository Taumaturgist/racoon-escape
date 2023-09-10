using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Traffic
{
    public class TrafficCarMovement : MonoBehaviour
    {
        [HideInInspector] public TrafficCarState State;
        private float _speed;

        private void Start()
        {
            _speed = Random.Range(5.0f, 6.0f);
            State = TrafficCarState.Rides;
        }
        private void Update()
        {
            if (State == TrafficCarState.Rides)
            {
                if (transform.position.x < 0)
                {
                    transform.Translate(-transform.TransformDirection(Vector3.forward) * _speed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(transform.TransformDirection(Vector3.forward) * _speed * Time.deltaTime);
                }
            }
        }
    }
}