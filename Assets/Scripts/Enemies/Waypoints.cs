using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Waypoints : MonoBehaviour
    {
        public List<SingleWaypoints> AllWaypoints;
        public Dictionary<int, float> WaypointsDistance { get; private set; }

        private void Awake()
        {
            WaypointsDistance = new Dictionary<int, float>();
            CalculateWaypointsDistance();
        }

        [ContextMenu("Fill waypoints")]
        public void FillWaypoints()
        {
            AllWaypoints.Clear();
            var paths = GameObject.FindGameObjectsWithTag("Way").ToList();
            foreach (var path in paths)
            {
                var list = path.transform.Cast<Transform>().ToList();
                AllWaypoints.Add(new SingleWaypoints(list));
            }
        }

        private void CalculateWaypointsDistance()
        {
            var pathsCount = AllWaypoints.Count;
            var distance = 0f;
            foreach (var sw in AllWaypoints)
            {
                for (var i = 0; i < sw.Waypoints.Count; i++)
                {
                    if (i == 0) continue;
                    distance += Vector3.Distance(sw.Waypoints[i].position, sw.Waypoints[i - 1].position);
                }
                WaypointsDistance[pathsCount] = distance;
                pathsCount++;
            }
        }

        public List<Transform> GetFlattenListOfAllWaypoints()
        {
            return AllWaypoints.SelectMany(d => d.Waypoints).ToList();
        }
    }

    [Serializable]
    public class SingleWaypoints
    {
        public List<Transform> Waypoints;
        public SingleWaypoints(List<Transform> list)
        {
            Waypoints = list;
        }
    }
}