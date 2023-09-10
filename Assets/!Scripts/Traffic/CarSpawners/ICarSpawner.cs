
using UnityEngine;

namespace Traffic
{
    public interface ICarSpawner
    {
        TrafficCar Spawn(Transform spawnTransform);
    }
}