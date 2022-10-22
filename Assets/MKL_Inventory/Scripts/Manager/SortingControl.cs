using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKL.Inventory
{
    public static class SortingControl 
    {
        public static int ShortByName(GameObject a, GameObject b)
        {
            return a.name.CompareTo(b.name);
        }
    }
}