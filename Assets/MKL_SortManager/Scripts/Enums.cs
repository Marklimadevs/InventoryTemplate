using System;
using UnityEngine;
using System.Collections.Generic;

namespace MKL.SortManager
{
    public class Enums
    {        
        public enum SortingType
        {
            None = 0,
            Name = 1,
            Category = 2,
            Weight = 3
        }
        public static List<GameObject>  SortByNameAscending(List<GameObject> partList)
        {
            List<GameObject>  parts = partList;

            parts.Sort(delegate (GameObject x, GameObject y)
            {
                if (x.name == null && y.name == null) return 0;
                else if (x.name == null) return -1;
                else if (y.name == null) return 1;
                else return x.name.CompareTo(y.name);
            });

            return parts;
        }
    }
}