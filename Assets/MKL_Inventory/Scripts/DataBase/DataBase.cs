using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.TextCore.Text;

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

        [ContextMenu("Get All Items In Project")]
        public void GetAllItemsInProject() 
        {           
            Items.Clear();

            string[] assetNames = AssetDatabase.FindAssets("Item", null);
            Items.Clear();
            foreach (string SOName in assetNames)
            {
                var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
                var character = AssetDatabase.LoadAssetAtPath<ItemBase>(SOpath);
                if (character != null) 
                {
                    Items.Add(character);
                }
            }
        }
        public static T[] GetAllInstances<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
            T[] a = new T[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;

        }
        internal void Clear()
        {
            Items.Clear();
        }

        private void OnValidate()
        {

        }

#endif
        #endregion

    }
}
