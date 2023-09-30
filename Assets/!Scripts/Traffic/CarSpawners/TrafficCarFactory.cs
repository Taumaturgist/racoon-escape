using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace Traffic
{
    public abstract class TrafficCarFactory
    {
        private List<TrafficCar> _prefabs;
        
        public TrafficCarFactory(List<TrafficCar> prefabs)
        {
            _prefabs = prefabs;
        }

        public TrafficCar Spawn(Vector3 position)
        {
            var trafficCar = Object.Instantiate(_prefabs.RandomItem(), position, Quaternion.identity);
            trafficCar.Launch();
            
            return trafficCar;
        }
        
        public TrafficCar Spawn(Vector3 position, Quaternion rotation)
        {
            var trafficCar = Object.Instantiate(_prefabs.RandomItem(), position, rotation);
            trafficCar.Launch();
            
            return trafficCar;
        }

        public TrafficCar Spawn(Vector3 position, Quaternion rotation, Transform parent)
        {
            var trafficCar = Object.Instantiate(_prefabs.RandomItem(), position, rotation, parent);
            trafficCar.Launch();

            return trafficCar;
        }
    }
}