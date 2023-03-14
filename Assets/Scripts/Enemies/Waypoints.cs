using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Waypoints : MonoBehaviour
    {
        public List<SingleWaypoints> AllWaypoints;

        [ContextMenu("Fill waypoints")]
        public void FillWaypoints()
        {
            AllWaypoints.Clear();
            var paths = GameObject.FindGameObjectsWithTag("Way").ToList();
            foreach (var path in paths)
            {
                var list = new List<Transform>();
                foreach (Transform child in path.transform)
                {
                    list.Add(child);
                }
                var sw = new SingleWaypoints(list);
                AllWaypoints.Add(sw);
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