using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class CityCarSpawner : ICarSpawner
    {
        private TrafficConfig _trafficConfig;
        private List<TrafficCar> _cars = new();
        private Tile _tile;
        private GameObject _traffic;

        public CityCarSpawner(TrafficConfig trafficConfig, Tile tile)
        {
            _trafficConfig = trafficConfig;
            _tile = tile;

            _traffic = new GameObject("Traffic");
            _traffic.transform.SetParent(tile.transform);

            for (int i = 0; i < _trafficConfig.CitySpawnPoints.Length; i += 2)
            {
                var randPointIndex = Random.Range(i, (i + 1) + 1);
                var car = Spawn(_trafficConfig.CitySpawnPoints[randPointIndex]);
                _cars.Add(car);
            }
        }

        public TrafficCar Spawn(Transform spawnTransform)
        {
            var randCarIndex = Random.Range(0, _trafficConfig.CityTraffic.Length);
            var trafficCar = Object
                .Instantiate(
                _trafficConfig.CityTraffic[randCarIndex].TrafficCar,
                _tile.transform.position + spawnTransform.position,
                spawnTransform.rotation,
                _traffic.transform);

            return trafficCar;
        }
    }
}