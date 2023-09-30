using System;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    [CreateAssetMenu(fileName = "TrafficConfig", menuName = "Configs/TrafficConfig")]
    public class TrafficConfig : ScriptableObject
    {
        [Header("TrafficCars")]
        public List<TrafficCar> CityTraffic;
        public List<TrafficCar> DesertTraffic;
        public List<TrafficCar> ForestTraffic;
        public List<TrafficCar> HighwayTraffic;
    }

    // public enum eTrafficCar
    // {
    //     Coupe,
    //     Minivan,
    //     Pickup,
    //     Sedan,
    //     Suv,
    //     Taxi,
    //     Police
    // }

    /*[Serializable]
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
    }*/
}