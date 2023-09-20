using UnityEngine;

namespace Traffic
{
    public class TrafficCar : MonoBehaviour
    {
        private TrafficCarMovement _carMovement;
        private LayerMask _layerMask;
        private readonly float _maxDistance = 8f;
        private Transform _direction;
        private int _laneNumber;

        public void Launch(int laneNumber)
        {
            _laneNumber = laneNumber;
        }

        private void Start()
        {
            _carMovement = GetComponent<TrafficCarMovement>();

            _layerMask = LayerMask.GetMask(new[] { "Player" });
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
                _carMovement.State = TrafficCarState.Standing;
            }

            if (transform.position.y < 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("TransitionTile"))
            {
                if (_laneNumber == 2 || _laneNumber == -2)
                {
                    transform.Rotate(0f, -45f, 0f);
                    if (transform.position.x == 1.85)
                    {
                        transform.Rotate(0f, 45f, 0f);
                    }
                }
            }
        }
    }
}