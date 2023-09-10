using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class DesertCarSpawner : ICarSpawner
    {
        private TrafficConfig _trafficConfig;
        private List<TrafficCar> _cars = new();
        private Tile _tile;
        private GameObject _traffic;

        public DesertCarSpawner(TrafficConfig trafficConfig, Tile tile)
        {
            _trafficConfig = trafficConfig;
            _tile = tile;

            _traffic = new GameObject("Traffic");
            _traffic.transform.SetParent(tile.transform);

            for (int i = 0; i < _trafficConfig.DesertSpawnPoints.Length; i++)
            {
                var car = Spawn(_trafficConfig.DesertSpawnPoints[i]);
                _cars.Add(car);
            }
        }

        public TrafficCar Spawn(Transform spawnTransform)
        {
            var randCarIndex = Random.Range(0, _trafficConfig.DesertTraffic.Length);
            var trafficCar = Object
                .Instantiate(
                _trafficConfig.DesertTraffic[randCarIndex].TrafficCar,
                _tile.transform.position + spawnTransform.position,
                spawnTransform.rotation,
                _traffic.transform);

            return trafficCar;
        }
    }
}