using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts.Enemies
{
    public class Waypoints : MonoBehaviour
    {
        public List<SingleWaypoints> AllWaypoints;
        public static Dictionary<int, float> WaypointsDistance { get; private set; }

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
                foreach (var e in list)
                {
                    e.TryGetComponent<MeshFilter>(out var mf);
                    e.TryGetComponent<BoxCollider>(out var mc);
                    e.TryGetComponent<MeshRenderer>(out var mr);
                    DestroyImmediate(mf);
                    DestroyImmediate(mr);
                    DestroyImmediate(mc);
                }
                AllWaypoints.Add(new SingleWaypoints(list));
            }
        }

        private void CalculateWaypointsDistance()
        {
            var pathsCount = 0;
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