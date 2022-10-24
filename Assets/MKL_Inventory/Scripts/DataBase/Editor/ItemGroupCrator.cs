using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MKL.Inventory
{
#if UNITY_EDITOR
    public class ItemGroupCrator : MonoBehaviour
    {
        [SerializeField] private const string DefaultPath = "@UntilThelastZombie/Modules/MKL_Inventory/Exemple/Scriptables/Items";
        [SerializeField] private DataBase _database;
        [SerializeField] private InventoryBase _InventoryBase;
        [SerializeField] private string Path;
        [Space(2)]
        [Header("Create Multiples Item")]
        [SerializeField] List<Sprite> IconGroup;

        [Space(2)]
        [Header("Create Single Item")]
        [SerializeField] List<ItemCreator> Creator;

        [Space(2)]
        [Header("Create By Item List")]
        [SerializeField] private List<ItemBase> ItemList;

        public void CreateItem()
        {
            foreach (var item in Creator)
            {
                if (item != null)
                {
                    item.CreateNewItem(DefaultPath);
                }
            }
        }
        public void CreateItemGroup()
        {
            CreateNewItemByImage(DefaultPath, _database, _InventoryBase);
        }
        private void CreateNewItemByImage(string Path, DataBase _database = null, InventoryBase _inventoryBase = null)
        {
            foreach (var item in IconGroup)
            {
                ItemBase asset = new ItemBase(item.name, "Generic", 0, true, item);

                AssetDatabase.CreateAsset(asset, $"Assets/{Path}/Item_Generic_{item.name}.asset");
                AssetDatabase.SaveAssets();
                if (_database != null)
                {
                    _database.AddItem(asset);
                }
                if (_inventoryBase != null)
                {
                    _inventoryBase.AddItem(asset);
                }
                //EditorUtility.FocusProjectWindow();

                //Selection.activeObject = asset;
            }
        }

        public void AddToInventoryByItemList()
        {
            if (_InventoryBase != null)
            {
                foreach (var item in ItemList)
                {
                    _InventoryBase.AddItem(item);
                }
                ItemList.Clear();
            }
        }
        public void AddToInventory()
        {
            foreach (var item in Creator)
            {
                if (item != null)
                {
                    item.AddToInventory(_InventoryBase);
                }
            }
        }
        public void AddToDataBase()
        {
            foreach (var item in Creator)
            {
                if (item != null)
                {
                    item.AddToDataBase(_database);
                }
            }
        }
    }


    [CustomEditor(typeof(ItemGroupCrator))]
    class ItemGroupCratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ItemGroupCrator itemcreator = (ItemGroupCrator)target;
            if (GUILayout.Button("Add to Inventory By Item list"))
            {
                itemcreator.AddToInventoryByItemList();
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Only Add to Inventory"))
            {
                itemcreator.AddToInventory();
            }
            if (GUILayout.Button("Only Add to Database"))
            {
                itemcreator.AddToDataBase();
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Single Item"))
            {
                itemcreator.CreateItem();
            }
            if (GUILayout.Button("Create Multiples Image"))
            {
                itemcreator.CreateItemGroup();
            }
            GUILayout.EndHorizontal();
        }
    }
    //[CustomEditor(typeof(ItemCreator))]
    //class ItemCreatorEditor : Editor 
    //{
    //    public override void OnInspectorGUI()
    //    {
    //        base.OnInspectorGUI();

    //        ItemCreator itemcreator = (ItemCreator)target;
    //        if (GUILayout.Button("Create Item"))
    //        {
    //            itemcreator.CreateMyAsset();
    //        }            
    //    }
    //}
#endif
}
