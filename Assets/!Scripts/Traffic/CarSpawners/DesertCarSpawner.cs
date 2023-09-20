using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class DesertCarSpawner : CarSpawner
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
            SelectPoints();
        }

        private void SelectPoints()
        {
            for (int i = 0; i < _trafficConfig.DesertPoints.Length; i++)
            {
                if (i + 1 == _trafficConfig.DesertPoints.Length)
                {
                    return;
                }
                    
                var randPointIndex = Random.Range(i, (i + 1) + 1);
                var car = Spawn(_trafficConfig.DesertPoints[randPointIndex]);
                _cars.Add(car);
            }
        }

        public override TrafficCar Spawn(Vector3 spawnPoint)
        {
            var randCarIndex = Random.Range(0, _trafficConfig.DesertTraffic.Length);
            var trafficCar = Object
                .Instantiate(
                    _trafficConfig.DesertTraffic[randCarIndex].TrafficCar,
                    _tile.transform.position + spawnPoint,
                    spawnPoint.x < 0 ? Quaternion.Euler(0f, -180f, 0f) : Quaternion.identity,
                    _traffic.transform);

            return trafficCar;
        }
    }
}