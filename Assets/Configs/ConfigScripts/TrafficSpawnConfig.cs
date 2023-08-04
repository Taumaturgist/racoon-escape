using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrafficSpawnConfig", menuName = "Configs/TrafficSpawnConfig", order = 51)]
public class TrafficSpawnConfig : ScriptableObject
{
    public TrafficCarsSpawner TrafficCarsSpawner;
    public List<GameObject> TrafficCars;
    public float TrafficCarsSpeed;
}
