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
            for (int i = 0; i < _trafficConfig.ForestPoints.Length; i++)
            {
                if (i + 1 == _trafficConfig.ForestPoints.Length)
                {
                    return;
                }
                    
                var randPointIndex = Random.Range(i, (i + 1) + 1);
                var car = Spawn(_trafficConfig.ForestPoints[randPointIndex]);
                _cars.Add(car);
            }
        }

        public override TrafficCar Spawn(Vector3 spawnPoint)
        {
            var randCarIndex = Random.Range(0, _trafficConfig.ForestTraffic.Length);
            var trafficCar = Object
                .Instantiate(
                    _trafficConfig.ForestTraffic[randCarIndex].TrafficCar,
                    _tile.transform.position + spawnPoint,
                    spawnPoint.x < 0 ? Quaternion.Euler(0f, -180f, 0f) : Quaternion.identity,
                    _traffic.transform);

            return trafficCar;
        }
    }
}