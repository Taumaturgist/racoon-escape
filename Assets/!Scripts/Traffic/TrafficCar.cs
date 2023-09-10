using UnityEngine;

namespace Traffic
{
    public class TrafficCar : MonoBehaviour
    {
        private TrafficCarMovement _carMovement;
        private LayerMask _layerMask;
        private float _maxDistance = 20f;
        private Transform _direction;

        private void Start()
        {
            _carMovement = GetComponent<TrafficCarMovement>();

            _layerMask = LayerMask.GetMask("Player");
            _direction = transform.GetChild(0).transform;
        }
        private void FixedUpdate()
        {
            RaycastHit hit;

            if (Physics.Raycast(
                _direction.position,
                _direction.TransformDirection(Vector3.forward),
                out hit,
                _maxDistance,
                _layerMask))
            {
                StandingLogic();
            }
        }

        private void StandingLogic()
        {
            _carMovement.State = TrafficCarState.Standing;
        }
    }
}