using UnityEngine;

namespace Traffic
{
    public class TrafficCar : MonoBehaviour
    {
        private TrafficCarMovement _carMovement;
        private LayerMask _layerMaskPlayer;
        private LayerMask _layerMaskTraffic;
        private readonly float _maxDistance = 15f;
        private Transform _direction;

        private void Start()
        {
            _carMovement = GetComponent<TrafficCarMovement>();

            _layerMaskPlayer = LayerMask.GetMask(new[] { "Player" });
            _layerMaskTraffic = LayerMask.GetMask(new[] { "Traffic" });
            _direction = transform.GetChild(0).transform;
        }

        private void FixedUpdate()
        {
            DetectPlayer();
            DetectTrafficCar();

            if (transform.position.y < 0)
            {
                Destroy(gameObject);
            }
        }

        private void DetectTrafficCar()
        {
            RaycastHit hit;

            if (Physics.Raycast(
                    _direction.position,
                    _direction.TransformDirection(Vector3.forward),
                    out hit,
                    _maxDistance,
                    _layerMaskTraffic))
            {
                _carMovement.State = TrafficCarState.Standing;
            }
        }

        private void DetectPlayer()
        {
            RaycastHit hit;

            if (Physics.Raycast(
                    _direction.position,
                    _direction.TransformDirection(Vector3.forward),
                    out hit,
                    _maxDistance,
                    _layerMaskPlayer))
            {
                _carMovement.State = TrafficCarState.Standing;
            }
        }
    }
}