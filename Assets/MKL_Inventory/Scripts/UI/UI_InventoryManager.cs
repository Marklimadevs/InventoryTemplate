using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MKL.Inventory;

namespace MKL.InventoryUI
{
    /// <summary>
    /// Controlador de Multiplos inventarios.
    /// </summary>
    public class UI_InventoryManager : MonoBehaviour
    {
        [SerializeField] private DataBase _database;
        [Space(2)]
        [SerializeField] List<UI_Inventory> InventoryList = new List<UI_Inventory>();

        private void Start()
        {
            foreach (var item in InventoryList)
            {
                item.onOpenInventory += OnUpdateInventorys;
                item.onAddedItem += OnItemAdded;
                item.onRemovedItem += OnRemovedItem;
            }
        }
        public void AddItem(string IdItem)
        {
            int.TryParse(IdItem, out int _Count);
            if (InventoryList[0].AddItem(_database.GetItem(_Count), 1))
            {
                OnUpdateInventorys(0);
            }
        }
        public void RemoveItem(string IdItem)
        {
            int.TryParse(IdItem, out int _Count);
            if (InventoryList[0].RemoveItem(_database.GetItem(_Count), 1))
            {
                OnUpdateInventorys(0);
            }
        }
        public void TransfenderItem(Item item, int Count = 1)
        {
            //Debug.Log($"TransfenderItem {item.Name} bagId:({item.BagId}) Count:({Count})");
            if (item != null)
            {
                int REMOVETO = item.BagId == 0 ? 0 : 1;
                int ADDTO = item.BagId == 1 ? 0 : 1;

                if (InventoryList[ADDTO].AddItem(item, Count))
                {
                    //Debug.Log($"ADDED {item.Name} ({Count})");
                    if (InventoryList[REMOVETO].RemoveItem(item, Count))
                    {
                        //Debug.Log($"REMOVED {item.Name} ({Count})");
                    }
                    else
                    {
                        //Debug.Log($"NOT REMOVED {item.Name} ({Count})");
                    }
                    //OnUpdateInventorys(ADDTO);
                    //OnUpdateInventorys(REMOVETO);
                }
                else
                {
                    //Debug.Log($"NOT ADDED {item.Name} ({Count})");
                }
            }
        }
        public void UpdateInventory(Inventory.Inventory inv)
        {
            InventoryList[1].UpdateInventory(inv);
        }

        #region Updates ---------------------------------------
        public void UpdateInventorys()
        {
            //Debug.Log($"UpdateInventorys");
            foreach (var item in InventoryList)
            {
                if(item != null)
                {
                    //Debug.Log($"UpdateInventorys {item._inventory.Name}");
                    item.UpdateInventory(item._inventory);
                }
            }
        }
        public void OnUpdateInventorys(int id)
        {
            //Debug.Log($"OnUpdateInventorys {id}");
            InventoryList[id].UpdateInventory(InventoryList[id]._inventory);
        }
        public void OnLoadedInventory(int id)
        {
            //Debug.Log($"OnLoaded {id}");
        }
        public void OnItemAdded()
        {
            //Debug.Log("OnItemAdded");
            UpdateInventorys();
        }
        public void OnRemovedItem()
        {
            //Debug.Log("OnRemovedItem");
            UpdateInventorys();
        }
        #endregion

#if UNITY_EDITOR
        #region Editor --------------------------------------

        private void OnValidate()
        {

        }

        #endregion
#endif
    }
}
