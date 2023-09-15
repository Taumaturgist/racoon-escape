using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class HighwayCarSpawner : CarSpawner
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

            SelectPoints();
        }

        private void SelectPoints()
        {
            for (int i = 0; i < _trafficConfig.HighwayPoints.Length; i += 3)
            {
                var randPointIndex = Random.Range(i, (i + 2) + 1);
                var car = Spawn(_trafficConfig.HighwayPoints[randPointIndex]);
                _cars.Add(car);
            }
        }

        public override TrafficCar Spawn(Vector3 spawnPoint)
        {
            var randInt = Random.Range(0, _trafficConfig.HighwayTraffic.Length);
            var trafficCar = Object
                .Instantiate(
                    _trafficConfig.HighwayTraffic[randInt].TrafficCar,
                    _tile.transform.position + spawnPoint,
                    spawnPoint.x < 0 ? Quaternion.Euler(0f, -180f, 0f) : Quaternion.identity,
                    _traffic.transform);

            return trafficCar;
        }
    }
}