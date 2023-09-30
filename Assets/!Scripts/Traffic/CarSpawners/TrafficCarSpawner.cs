using System;
using System.Collections.Generic;
using Extensions;
using Traffic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrafficCarSpawner : MonoBehaviour
{
    private const int MIN_SPAWN_CARS = 1;
    private const int MAX_SPAWN_CARS = 8;
    [SerializeField] private Transform[] startWaypoints, endWaypoints;
    [SerializeField] private int amountSpawns;
    private TrafficCarFactory _trafficCarFactory;
    private List<Vector3> _spawnPositions;

    public void Launch(TrafficConfig trafficConfig, eBlockType blockType)
    {
        _trafficCarFactory = GetFactory(blockType, trafficConfig);
        
        if (_trafficCarFactory == null)
            return;

        _spawnPositions = GetSpawnPoints();
        
        for (int i = 1; i <= Random.Range(MIN_SPAWN_CARS, MAX_SPAWN_CARS); i++)
        {
            Spawn();
        }
    }
    
    private TrafficCarFactory GetFactory(eBlockType blockType, TrafficConfig trafficConfig)
    {
        switch (blockType)
        {
            case eBlockType.City:
                return new CityCarFactory(trafficConfig.CityTraffic);
            case eBlockType.Desert:
                return new DesertCarFactory(trafficConfig.DesertTraffic);
            case eBlockType.Forest:
                return new ForestCarFactory(trafficConfig.ForestTraffic);
            case eBlockType.Highway:
                return new HighwayCarFactory(trafficConfig.HighwayTraffic);
            default:
                throw new ArgumentOutOfRangeException(nameof(blockType), blockType, null);
        }
    }

    private List<Vector3> GetSpawnPoints()
    {
        List<Vector3> spawnPoints = new List<Vector3>();
        List<TrafficLane> trafficLanes = new List<TrafficLane>();

        for (int i = 0; i < startWaypoints.Length; i++)
        {
            if (endWaypoints == null)
                break;

            trafficLanes.Add(new TrafficLane(startWaypoints[i].position,
                endWaypoints[i].position));
        }

        foreach (var lane in trafficLanes)
        {
            foreach (var point in lane.GetPoints(amountSpawns))
            {
                spawnPoints.Add(point);
            }
        }

        return spawnPoints;
    }

    private void Spawn()
    {
        var spawnPosition = _spawnPositions.RandomItem();
        var trafficCar = _trafficCarFactory.Spawn(spawnPosition,
            spawnPosition.x < 0f ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.identity,
            transform);
        
        _spawnPositions.Remove(spawnPosition);
    }

    /*private void OnDrawGizmos()
    {
        List<TrafficLane> lanes = new List<TrafficLane>();
        Gizmos.color = Color.red;
        for (int i = 0; i < startWaypoints.Length; i++)
        {
            if (startWaypoints == null || endWaypoints == null)
                continue;

            Gizmos.DrawLine(startWaypoints[i].position, endWaypoints[i].position);
            lanes.Add(new TrafficLane(startWaypoints[i].position,
                endWaypoints[i].position));
        }

        Gizmos.color = Color.white;
        foreach (var lane in lanes)
        {
            foreach (var point in lane.GetPoints(amountSpawns))
            {
                Gizmos.DrawSphere(point, 0.5f);
            }
        }
    }*/
}