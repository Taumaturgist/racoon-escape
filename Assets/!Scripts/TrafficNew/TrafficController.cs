using UnityEngine;

namespace Traffic
{
    public class TrafficController
    {
        private TrafficConfig _trafficConfig;
        private ITrafficFactory _trafficFactory;
        private TrafficView _trafficView;
        private GameObject _trafficPrefab;
        private Vector3 _startPos;

        public TrafficController(TrafficConfig trafficConfig)
        {
            _trafficConfig = trafficConfig;

            // _trafficPrefab = new GameObject("TrafficPrefab");
            _trafficFactory = new TrafficFactory(_trafficConfig);

            for (int i = 0; i < _trafficConfig.CitySpawnPoints.Length; i += 3)
            {
                Spawn(i);
            }
        }

        private void Spawn(int randomPoint)
        {
            var trafficModel = _trafficConfig.TrafficModel;
            // var randomPoint = Random.Range(0, _trafficConfig.CitySpawnPoints.Length);

            _trafficView = _trafficFactory.Create(trafficModel[0], _trafficView,
                _trafficConfig.CitySpawnPoints[randomPoint]);
        }
    }
}