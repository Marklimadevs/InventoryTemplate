using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MKL.Inventory
{
    [CreateAssetMenu(fileName = "New DataBase",
     menuName = "MKL/Inventory/New Data Base")]
    public class DataBase : ScriptableObject
    {
        public List<ItemBase> Items;

        public void AddItem(ItemBase newItemBase) 
        {
            Items.Add(newItemBase);
        }
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

        #region Editor --------------------------------------

#if UNITY_EDITOR

        private void OnValidate()
        {

        }

        internal void Clear()
        {
            Items.Clear();
        }

#endif
        #endregion

    }
}
