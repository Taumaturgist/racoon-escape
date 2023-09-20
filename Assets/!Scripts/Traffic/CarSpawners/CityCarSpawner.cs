using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class CityCarSpawner : CarSpawner
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

            SelectPoints();
        }

        private void SelectPoints()
        {
            for (int i = 0; i < _trafficConfig.CityPoints.Length; i += 2)
            {
                var randPointIndex = Random.Range(i, (i + 1) + 1);
                var car = Spawn(_trafficConfig.CityPoints[randPointIndex]);
                _cars.Add(car);
            }
        }

        public override TrafficCar Spawn(Vector3 spawnPoint)
        {
            var randCarIndex = Random.Range(0, _trafficConfig.CityTraffic.Length);
            var trafficCar = Object
                .Instantiate(
                    _trafficConfig.CityTraffic[randCarIndex].TrafficCar,
                    _tile.transform.position + spawnPoint,
                    spawnPoint.x < 0 ? Quaternion.Euler(0f, -180f, 0f) : Quaternion.identity,
                    _traffic.transform);

            int laneNumber = 0;

            if (spawnPoint.x < -1.85f)
            {
                laneNumber = -2;
            }
            else if (spawnPoint.x < 0f)
            {
                laneNumber = -1;
            }
            
            if (spawnPoint.x > 1.85f)
            {
                laneNumber = 2;
            }
            else if (spawnPoint.x > 0f)
            {
                laneNumber = 1;
            }
            
            trafficCar.Launch(laneNumber);

            return trafficCar;
        }
    }
}