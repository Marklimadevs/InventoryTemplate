using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MKL.Utilities
{
    public static class ConvertFormats
    {
        public static string ReturnKgValue(float value)
        {
            float newvalue = value;
            string typevalue = "Kg";

            if (value < 1000)
            {
                newvalue = (float)Math.Round(newvalue, 2);
                typevalue = "Kg";
            }
            if (value >= 1000)
            {
                newvalue = (float)Math.Round((newvalue / 1000), 2);
                typevalue = "T";
            }

            return $"{newvalue} {typevalue}";
        }
        public static string ReturnUnValue(float value)
        {
            float newvalue = (int)value;
            string typevalue = ""; 

            if (value < 999)
            {
                //newvalue = newvalue;
                typevalue = "";
            }
            else 
            {
                newvalue = (float)Math.Round(newvalue, 2);
                typevalue = "";
            }                       
            return $"{newvalue} {typevalue}";

        }
    }
}
