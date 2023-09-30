using System.Collections.Generic;
using UnityEngine;


    public class TrafficLane
    {
        private Vector3 _startWaypoint;
        private Vector3 _endWaypoint;

        public TrafficLane(Vector3 startWaypoint, Vector3 endWaypoint)
        {

            _startWaypoint = startWaypoint;
            _endWaypoint = endWaypoint;
        }

        public IEnumerable<Vector3> GetPoints(int step)
        {
            float length = (_startWaypoint - _endWaypoint).magnitude;
            var result = new List<Vector3>();
            for (int i = 0; i < step; i++)
            {
                result.Add(new Vector3(_startWaypoint.x,
                    0,
                    _startWaypoint.z + (length / (step - 1)) * i));
            }

            return result;
        }
    }

