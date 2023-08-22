using UnityEngine;

namespace Traffic
{
    public class TrafficController
    {
        private TrafficConfig _trafficConfig;
        private GameObject _trafficPrefab;
        private Vector3 _startPos;
        
        public TrafficController(TrafficConfig trafficConfig)
        {
            _trafficConfig = trafficConfig;
            _trafficPrefab = new GameObject("TrafficPrefab");
        }

        private TrafficView SpawnTrafficCar()
        {
            
            
            return null;
        }
    }
}
