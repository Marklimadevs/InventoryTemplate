using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

namespace MKL.Inventory
{
    [System.Serializable]
    public class ItemCreator 
    {                
        [Space(1)]
        [Header("-----Geral Config-----")]
        [SerializeField] private string _itemName;
        [SerializeField] private string _itemCategory;
        [SerializeField] private float _weight = 0f;
        [SerializeField] private bool _IsStackable = true;
        [SerializeField] private Sprite _UiIcon;

#if UNITY_EDITOR
        
        public void AddToInventory(InventoryBase _inventoryBase) 
        {
            if (_inventoryBase != null)
            {
                _inventoryBase.AddItem(new ItemBase(_itemName, _itemCategory, _weight, _IsStackable, _UiIcon));
            }
        }
        public void AddToDataBase(DataBase _database)
        {
            if (_database != null)
            {
                _database.AddItem(new ItemBase(_itemName, _itemCategory, _weight, _IsStackable, _UiIcon));
            }
        }
        public void CreateNewItem(string Path, DataBase _database = null, InventoryBase _inventoryBase = null)
        {            
            ItemBase asset = new ItemBase(_itemName, _itemCategory, _weight, _IsStackable, _UiIcon);

            AssetDatabase.CreateAsset(asset, $"Assets/{Path}/Item_{_itemCategory}_{_itemName}.asset");
            AssetDatabase.SaveAssets();
            if(_database != null) 
            {
                AddToDataBase(_database);
            }
            if (_inventoryBase != null) 
            {
                AddToInventory(_inventoryBase);
            }

            //EditorUtility.FocusProjectWindow();

                //Selection.activeObject = asset;
        }
        public void CreateItemByItemBase(string Path, DataBase _database = null, InventoryBase _inventoryBase = null)
        {
            ItemBase asset = new ItemBase(_itemName, _itemCategory, _weight, _IsStackable, _UiIcon);

            AssetDatabase.CreateAsset(asset, $"Assets/{Path}/Item_{_itemCategory}_{_itemName}.asset");
            AssetDatabase.SaveAssets();
            if (_database != null)
            {
                AddToDataBase(_database);
            }
            if (_inventoryBase != null)
            {
                AddToInventory(_inventoryBase);
            }

            //EditorUtility.FocusProjectWindow();

            //Selection.activeObject = asset;
        }

#endif

    }
}
