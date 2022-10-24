using MKL.Inventory;
using UnityEditor;
using UnityEngine;

namespace MKL.Inventory
{
#if UNITY_EDITOR
    [CustomEditor(typeof(DataBase))]
    public class DataBaseEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DataBase itemcreator = (DataBase)target;
            if (GUILayout.Button("Clear Inventory"))
            {
                itemcreator.Clear();
            }
            base.OnInspectorGUI();
        }
    }
#endif
}
