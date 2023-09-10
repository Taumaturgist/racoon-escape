using System;
using UnityEngine;

namespace Traffic
{
    [CreateAssetMenu(fileName = "TrafficConfig", menuName = "Configs/TrafficConfig")]
    public class TrafficConfig : ScriptableObject
    {
        [Header("TrafficCars")]
        public CityTraffic[] CityTraffic;
        public DesertTraffic[] DesertTraffic;
        public ForestTraffic[] ForestTraffic;
        public HighwayTraffic[] HighwayTraffic;

        [Header("SpawnPoints")]
        public Transform[] CitySpawnPoints;
        public Transform[] DesertSpawnPoints;
        public Transform[] ForestSpawnPoints;
        public Transform[] HighwaySpawnPoints;
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
    public struct CityTraffic
    {
        public eTrafficCar TrafficCarName;
        public TrafficCar TrafficCar;
    }
    [Serializable]
    public struct DesertTraffic
    {
        public eTrafficCar TrafficCarName;
        public TrafficCar TrafficCar;
    }
    [Serializable]
    public struct ForestTraffic
    {
        public eTrafficCar TrafficCarName;
        public TrafficCar TrafficCar;
    }
    [Serializable]
    public struct HighwayTraffic
    {
        public eTrafficCar TrafficCarName;
        public TrafficCar TrafficCar;
    }
}