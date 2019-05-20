using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DroidDigital.PacMan.PathFind
{
    internal static class WayPointManagement
    {
        public static List<ItemPath> WayPointList = new List<ItemPath>();
        
        public static Queue<ItemPath> WayPointQueue = new Queue<ItemPath>(); 

        public static void PopulatePathList()
        {
            var paths = GameObject.FindObjectsOfType<ItemPath>().ToList();

            if (paths == null || paths.Count == 0)
                throw new NullReferenceException("PATH NOT FINDED");
                  
            WayPointList.AddRange(paths);

            foreach (var v in WayPointList)
            {
                WayPointQueue.Enqueue(v);
            }
        }

        public static void PopulateQueue()
        {
            var list = WayPointList.OrderBy(e => e.PlayerDistance).ToList();

            foreach (var v in list)
                WayPointQueue.Enqueue(v);
          
        }


        public static ItemPath GetMostClosestPath(Vector3 currentPosition)
        {          
            var pos = WayPointList.TakeWhile(
                e => Vector3.Distance(currentPosition, e.PathTransform.position) <= 1.0F).ToList();

            if (pos != null && pos.Count > 0)
            {
                Debug.Log("Pegou o mais proximo: " + pos.Count);
                return pos[0];
            }
            
            Debug.Log("Pegou random");

            var randomList = WayPointList.OrderBy(e => Guid.NewGuid()).ToList();

            return randomList[0];
        }
    }
}