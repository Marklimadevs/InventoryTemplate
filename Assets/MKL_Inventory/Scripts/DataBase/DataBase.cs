using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MKL.Inventory
{
    [CreateAssetMenu(fileName = "New DataBase",
     menuName = "MKL/Inventory/New Data Base")]
    public class DataBase : ScriptableObject
    {
        public string FindItensInPath;
        public ItemBase[] Items;

        public ItemBase GetItembase(int index)
        {
            return Items[index];
        }
        public List<Item> GetItemList()
        {
            List<Item> newlist = new List<Item>();
            int i = 0;
            foreach (var item in Items)
            {
                newlist.Add(new Item(Items[i], 1, 0, 0));
                i++;
            }

            return newlist;
        }
        public Item GetItem(int index)
        {
            return new Item(Items[index], 1, 0, 0);
        }
        public ItemBase GetItem(ItemBase itemBase)
        {
            foreach (ItemBase item in Items)
            {
                if (itemBase.Name == item.Name)
                {
                    return item;
                }
            }
            return null;
        }

        #region Editor --------------------------------------

#if UNITY_EDITOR

        private void OnValidate()
        {

        }

#endif
        #endregion

    }
}
