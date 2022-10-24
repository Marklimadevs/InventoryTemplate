#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace MKL.Inventory
{
    [CustomEditor(typeof(Inventory))]
    public class InventoryEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Inventory inventory = (Inventory)target;

            GUILayout.BeginHorizontal();

            if(GUILayout.Button("Load inventory")) 
            {
                inventory.LoadInventory();
            }
            if (GUILayout.Button("Clear inventory"))
            {
                inventory.ClearInventory();
            }

            GUILayout.EndHorizontal();
        }
    }
}
#endif
