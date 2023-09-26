using UnityEngine;

namespace Traffic
{
    public abstract class CarSpawner
    {
        public abstract TrafficCar Spawn(Vector3 spawnPoint);
    }
}