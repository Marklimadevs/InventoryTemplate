#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MKL.Inventory
{
    [CustomEditor(typeof(InventoryBase))]
    class InventoryBaseEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            InventoryBase itemcreator = (InventoryBase)target;
            if (GUILayout.Button("Clear Inventory"))
            {
                itemcreator.Clear();
            }
            base.OnInspectorGUI();
        }
    }
}
#endif
