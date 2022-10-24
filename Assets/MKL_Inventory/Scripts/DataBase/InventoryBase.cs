using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MKL.Inventory
{
    /// <summary>
    /// O inventario base e usado para Guardar um Grupo de itens puxados da DataBase
    /// </summary>
    [CreateAssetMenu(fileName = "New Inventory Base",
     menuName = "MKL/Inventory/New Inventory Base ")]
    public class InventoryBase : ScriptableObject
    {
        [SerializeField] private bool Updating;
        [SerializeField] private DataBase _database;
        [SerializeField] private string _name;
        [SerializeField] private float _weight;
        [SerializeField] private float _weightMax = 100;
        [SerializeField] private List<Item> _ItemList;
        public string Name
        {
            get => _name;
        }
        public float Weight
        {
            get
            {
                UpdateWeight();
                return _weight;
            }
        }
        public float WeightMax
        {
            get => _weightMax;
        }
        public void UpdateWeight()
        {
            int i = 0;
            float WeightTotal = 0;
            //string _debug = "";

            foreach (Item item in _ItemList)
            {
                item.UpdateItemId(i);
                WeightTotal += item.WeightMax;
                //_debug += ($"instanced = {item.Name} / Weight = {item.WeightMax} Total = {WeightTotal}\n");
                i++;
            }

            _weight = (float)Math.Round(WeightTotal, 2);

            //Debug.Log(_debug);
        }
        public List<Item> ItemList
        {
            get => _ItemList;
        }
        public DataBase DataBase
        {
            get => _database;
        }
        public void AddItem(ItemBase newitem)
        {
            _ItemList.Add(new Item(newitem, 1, 0, _ItemList.Count));
        }
        public void Clear()
        {
            _ItemList = new List<Item>();
            UpdateWeight();
        }

        #region Editor --------------------------------------

#if UNITY_EDITOR

        private void OnValidate()
        {
            foreach (var item in ItemList)
            {
                if (Updating)
                {
                    item.LoadItem();
                    UpdateWeight();
                }
            }
        }

#endif
        #endregion
    }

}
