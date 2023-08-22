using UnityEngine;

namespace Traffic
{
    public class TrafficCarsSpawner : MonoBehaviour
    {
        private TrafficCar _trafficCarPrefab;
        [SerializeField] private Vector3[] trafficCarsSpawnPoints;

        private TrafficConfig _trafficConfig;
        private bool _isOnLeftStreetSide;

        public void Launch(TrafficConfig trafficConfig)
        {
            _trafficConfig = trafficConfig;
            // _trafficCarPrefab = _trafficConfig.TrafficCarPrefab;

            for (int i = 0; i < trafficCarsSpawnPoints.Length; i++)
            {
                var trafficCar = Instantiate(_trafficCarPrefab, transform.position + trafficCarsSpawnPoints[i], Quaternion.identity, transform);
                _isOnLeftStreetSide = trafficCarsSpawnPoints[i].x < 0;

                trafficCar.Launch(_trafficConfig, _isOnLeftStreetSide);
            }
        }
    }
}