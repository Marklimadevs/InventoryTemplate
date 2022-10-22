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
            StartCoroutine(LoadAfterSeconds());
        }
        public void TransfenderItem(Item item, int Count = 1)
        {
            //Debug.Log($"TransfenderItem {item.Name} bagId:({item.BagId}) Count:({Count})");

            if (item != null)
            {
                int REMOVETO = item.BagId == 0 ? 0:1;                
                int ADDTO = item.BagId == 1 ? 0:1;

                if (InventoryList[ADDTO].AddItem(item, Count))
                {
                    //Debug.Log($"ADDED {item.Name} ({Count})");
                    if(InventoryList[REMOVETO].RemoveItem(item, Count))
                    {
                        //Debug.Log($"REMOVED {item.Name} ({Count})");
                    }
                    else 
                    {
                        //Debug.Log($"NOT REMOVED {item.Name} ({Count})");
                    }
                    UpdateInventorys();
                }
                else
                {
                    //Debug.Log($"NOT ADDED {item.Name} ({Count})");
                }
            }
        }
        public void AddItem(string IdItem)
        {
            int.TryParse(IdItem, out int _Count);
            if (InventoryList[0].AddItem(_database.GetItem(_Count), 1))
            {
                UpdateInventorys();
            }
        }
        public void RemoveItem(string IdItem)
        {
            int.TryParse(IdItem, out int _Count);
            if (InventoryList[0].RemoveItem(_database.GetItem(_Count), 1))
            {
                UpdateInventorys();
            }
        }

        #region Updates ---------------------------------------
        public void UpdateInventorys()
        {
            foreach (var display in InventoryList)
            {
                if (display != null)
                {
                    display.UpdateInventory();
                    display.UpdateTexts();
                }
            }
        }
        IEnumerator LoadAfterSeconds() 
        {
            yield return new WaitForSeconds(0.1f);
            UpdateInventorys();
        }

        #endregion

        #region Editor --------------------------------------

#if UNITY_EDITOR

        private void OnValidate()
        {

        }

#endif
        #endregion
    }
}
