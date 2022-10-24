using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MKL.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [Space(1)]
        [Header("INVENTORY CONFIG------------------------------------")]
        [Space(1)]
        [SerializeField] private string _name;
        [SerializeField] private int _bagId;
        [SerializeField] private float _weight;
        [SerializeField] private float _weightMax = 100;
        [SerializeField] private InventoryBase _Inventorybase;

        [Space(1)]
        [Header("INVENTORY ITENS------------------------------------")]
        [Space(1)]

        [SerializeField] bool _loaded = false;
        [SerializeField] private List<Item> _ItemList;

        #region Propriedades
        public string Name
        {
            get => _name;
        }
        public int BagId
        {
            get => _bagId;
        }
        public float Weight
        {
            get
            {
                UpdateInventory();
                return _weight;
            }
        }
        public float WeightMax
        {
            get
            {
                return _weightMax;
            }
        }
        public bool Loaded
        {
            get => _loaded;
        }
        public List<Item> ItemList
        {
            get => _ItemList;
        }
        #endregion

        #region Metodos
        //Publics -----------------------------------------------------------------------
        public void AddItem(Item NewItem, int count, out bool _return)
        {
            float WeightTotal = count * NewItem.Weight;
            if (IsFull(WeightTotal))
            {
                _return = false;
            }
            else
            {
                if (NewItem.IsStackable && FindItem(NewItem, out Item FindedItem))
                {
                    FindedItem.Add(count);
                }
                else
                {
                    CreateNewItem(NewItem, count);
                }
                _return = true;
                UpdateInventory();
            }
        }
        public void RemoveItem(Item NewItem, int count, out bool _Return)
        {
            FindItem(NewItem, out Item findedItem);
            if (findedItem != null)
            {
                findedItem.Remove(count);
                if (findedItem.IsEmply)
                {
                    _ItemList.Remove(findedItem);
                }
                _Return = true;
                UpdateInventory();
            }
            else
            {
                Debug.Log($"Item not Founded to destroy = {NewItem.Name}");
                _Return = false;
            }
        }

        //Updates -----------------------------------------------------------------------
        public void LoadInventory()
        {
            int i = 0;
            foreach (Item item in _Inventorybase.ItemList)
            {
                if (!item.IsEmply)
                {
                    ItemList.Add
                    (
                        new Item(itemBase: item.itemBase, count: item.Count, bagId: BagId, ItemId: i)
                    );
                }
                i++;
            }

            UpdateInventory();

            _loaded = true;
        }
        public void LoadDebugInventory()
        {
            int i = 0;
            foreach (Item item in _Inventorybase.DataBase.GetItemList())
            {
                ItemList.Add
                (
                   new Item(itemBase: item.itemBase, count: item.Count, bagId: BagId, ItemId: i)
                );
                i++;
            }
            _weightMax = _Inventorybase.WeightMax;
            _name = _Inventorybase.Name;
            UpdateInventory();

            _loaded = true;
        }
        public void UpdateInventory()
        {
            _weightMax = _Inventorybase.WeightMax;
            _name = _Inventorybase.Name;

            int i = 0;
            float WeightTotal = 0;

            foreach (Item item in ItemList)
            {
                item.UpdateItemId(i);
                item.UpdateBagId(BagId);
                WeightTotal += item.WeightMax;
                i++;
            }

            _weight = (float)Math.Round(WeightTotal, 2);
        }
        public void ClearInventory()
        {
            _ItemList = new List<Item>();
            UpdateInventory();
        }

        //Privates -----------------------------------------------------------------------
        private bool FindItem(Item item, out Item FindedItem)
        {
            foreach (var _item in ItemList)
            {
                if (CompareItems(item, _item))
                {
                    FindedItem = _item;
                    return true;
                }
            }
            FindedItem = null;
            return false;
        }
        private bool CompareItems(Item item1, Item item2)
        {
            bool IsEqual = true;

            if
            (
                item1.Name != item2.Name ||
                item1.itemBase != item2.itemBase ||
                item1.Category != item2.Category ||
                item1.IsStackable != item2.IsStackable ||
                item1.DurabilityPorcent != item2.DurabilityPorcent
            )
            {
                IsEqual = false;
            }
            return IsEqual;
        }
        private bool IsFull(float value = 0)
        {
            if (Weight + value <= WeightMax)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void CreateNewItem(Item NewItem, int count)
        {
            ItemList.Add
            (
                new Item
                (
                    itemBase: NewItem.itemBase,
                    count: count,
                    bagId: BagId,
                    ItemId: _ItemList.Count
                )
            );
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
