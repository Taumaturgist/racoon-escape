using Traffic;
using UnityEngine;

public interface ITrafficFactory
{
    TrafficView Create(TrafficModel trafficModel, TrafficView trafficView, Vector3 position);
}