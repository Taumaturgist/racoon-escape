using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class HighwayCarSpawner : ICarSpawner
    {
        private TrafficConfig _trafficConfig;
        private List<TrafficCar> _cars = new();
        private Tile _tile;
        private GameObject _traffic;

        public HighwayCarSpawner(TrafficConfig trafficConfig, Tile tile)
        {
            _trafficConfig = trafficConfig;
            _tile = tile;

            _traffic = new GameObject("Traffic");
            _traffic.transform.SetParent(tile.transform);

            for (int i = 0; i < _trafficConfig.HighwaySpawnPoints.Length; i++)
            {
                var car = Spawn(_trafficConfig.HighwaySpawnPoints[i]);
                _cars.Add(car);
            }
        }

        public TrafficCar Spawn(Transform spawnTransform)
        {
            var randInt = Random.Range(0, _trafficConfig.HighwayTraffic.Length);
            var view = Object
                .Instantiate(
                _trafficConfig.HighwayTraffic[randInt].TrafficCar,
                _tile.transform.position + spawnTransform.position,
                spawnTransform.rotation,
                _traffic.transform);

            return view;
        }
    }
}