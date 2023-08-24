using System;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    [CreateAssetMenu(fileName = "TrafficConfig", menuName = "Configs/TrafficConfig", order = 51)]
    public class TrafficConfig : ScriptableObject
    {
        public TrafficModel[] TrafficModel => _trafficModel;

        [SerializeField] private TrafficModel[] _trafficModel;

        public Vector3[] CitySpawnPoints => citySpawnPoints;
        [SerializeField] private Vector3[] citySpawnPoints;
        
        // [SerializeField] private Vector3[] DesertSpawnPoints;
        // [SerializeField] private Vector3[] ForestSpawnPoints;
        // [SerializeField] private Vector3[] HighwaySpawnPoints;
    
        private Dictionary<Enum, TrafficView> _trafficDict = new();

        [NonSerialized] private bool _inited;

        private void Init()
        {
            foreach (var model in TrafficModel)
            {
                _trafficDict.Add(model.TrafficCarName, model.TrafficCar);
            }
        }

        public TrafficView Get(eTrafficCar trafficCarName)
        {
            if (!_inited)
            {
                Init();
            }

            if (!_trafficDict.ContainsKey(trafficCarName))
            {
                Debug.LogError($"Traffic car named {trafficCarName} not found");
            }
        
            return _trafficDict[trafficCarName];
        }
    }

    public enum eTrafficCar
    {
        Coupe,
        Minivan,
        Pickup,
        Sedan,
        Suv,
        Taxi,
        Police
    }

    [Serializable]
    public struct TrafficModel
    {
        public eTrafficCar TrafficCarName;
        public TrafficView TrafficCar;
    }
}