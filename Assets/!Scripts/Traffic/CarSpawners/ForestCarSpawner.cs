using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class ForestCarSpawner : CarSpawner
    {
        private TrafficConfig _trafficConfig;
        private List<TrafficCar> _cars = new();
        private Tile _tile;
        private GameObject _traffic;

        public ForestCarSpawner(TrafficConfig trafficConfig, Tile tile)
        {
            _trafficConfig = trafficConfig;
            _tile = tile;

            _traffic = new GameObject("Traffic");
            _traffic.transform.SetParent(tile.transform);

            SelectPoints();
        }

        private void SelectPoints()
        {
            for (int i = 0; i < _trafficConfig.ForestPoints.Length; i += 2)
            {
                var randPointIndex = Random.Range(i, (i + 1) + 1);
                var car = Spawn(_trafficConfig.ForestPoints[randPointIndex]);
                _cars.Add(car);
            }
        }

        public override TrafficCar Spawn(Vector3 spawnPoint)
        {
            var randInt = Random.Range(0, _trafficConfig.ForestTraffic.Length);

            var trafficCar = Object
                .Instantiate(
                    _trafficConfig.ForestTraffic[randInt].TrafficCar,
                    _tile.transform.position + spawnPoint,
                    Quaternion.identity,
                    _traffic.transform);

            return trafficCar;
        }
    }
}