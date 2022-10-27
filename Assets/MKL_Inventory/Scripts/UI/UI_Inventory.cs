using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MKL.InventoryUI
{
    using MKL.Inventory;
    using System;

    /// <summary>
    /// Representacao de um Inventario na UI
    /// </summary>
    public class UI_Inventory : MonoBehaviour
    {
        [Space(2)]
        [SerializeField] public Inventory _inventory;
        [Space(2)]
        [Header("Config -------------------------------")]
        [SerializeField] private UI_Item _itemTemplate;
        [SerializeField] private Transform _Content;
        [SerializeField] private Animator _Animator;
        [SerializeField] private TextMeshProUGUI _txtInventoryName;
        [SerializeField] private Toggle _toogle;
        [SerializeField][Range(0.5f, 2)] private float _AnimationSpeed = 1;

        [Space(2)]
        [Header("Soring Config ------------------------")]
        [SerializeField] SortingType _sortingType;
        [SerializeField] GroupType _groupType;

        #region Functions -----------------------------------
        public void Start()
        {
            UpdateInventory(_inventory);
        }
        public bool AddItem(Item item, int Value = 1)
        {
            if (_inventory != null)
            {
                _inventory.AddItem(item, Value, out bool _return);
                if (_return)
                {
                    OpenInventory();
                    if (onAddedItem != null)
                    {
                        onAddedItem.Invoke();
                    }
                }
                return _return;
            }
            else
            {
                return false;
            }
        }
        public bool RemoveItem(Item item, int Value = 1)
        {
            if (_inventory != null)
            {
                _inventory.RemoveItem(item, Value, out bool _return);
                if (_return)
                {
                    if (onRemovedItem != null)
                    {
                        onRemovedItem.Invoke();
                    }
                }
                return _return;
            }
            else
            {
                return false;
            }
        }
        public void OpenInventory()
        {
            ToggleInventory(true);
            if (_toogle != null)
            {
                _toogle.isOn = true;
            }
            if (onOpenInventory != null)
            {
                onOpenInventory.Invoke(_inventory.BagId);
            }
        }
        public void CloseInventory()
        {
            ToggleInventory(false);
            if (_toogle != null)
            {
                _toogle.isOn = false;
            }
        }
        public void ToggleInventory(bool IsActive)
        {
            if (_Animator != null)
            {
                _Animator.Play(IsActive ? "open" : "close");
                _Animator.speed = _AnimationSpeed;
            }
        }
        #endregion

        #region Updates --------------------------------------

        public void UpdateInventory(Inventory _inv = null)
        {
            foreach (Transform child in _Content)
            {
                Destroy(child.gameObject);
            }

            _inventory = _inv;
            if (_inventory != null)
            {
                if(!_inventory.Loaded)
                {
                    _inventory.LoadInventory();
                }

                if (onLoadedInventory != null)
                {
                    onLoadedInventory.Invoke(_inventory.BagId);
                }

                UpdateSorting();

                int i = 0;
                foreach (var item in _inventory.ItemList)
                {
                    if (item != null && !item.IsEmply)
                    {
                        UI_Item newItemDisplay = Instantiate(_itemTemplate, _Content);
                        newItemDisplay.Updateitem(item, i);
                    }
                    i++;
                }
            }
            UpdateTexts();
        }
        public void UpdateTexts()
        {
            if (_txtInventoryName != null)
            {
                if (_inventory != null)
                {
                    _txtInventoryName.SetText($"{_inventory.Name} ({_inventory.Weight}/{_inventory.WeightMax})");
                }
                else
                {
                    _txtInventoryName.SetText("");
                }
            }
        }

        #endregion

        #region Delegates -------------------------------------

        public delegate void OnLoadedInventory(int id);
        public event OnLoadedInventory onLoadedInventory;

        public delegate void OnOpenInventory(int id);
        public event OnOpenInventory onOpenInventory;
        public delegate void OnRemovedInventory();
        public event OnRemovedInventory onRemovedItem;

        public delegate void OnAddedItem();
        public event OnAddedItem onAddedItem;


        #endregion

        #region Sorting ----------------------
        public enum SortingType
        {
            None = 0,
            Name = 1,
            Category = 2,
            Weight = 3,
            Count = 4
        }
        public enum GroupType
        {
            None = 0,
            Name = 1,
            Category = 2,
            Weight = 3
        }
        public void SetSorting(int Value)
        {
            _sortingType = (SortingType)Value;
            UpdateSorting();
            UpdateInventory(this._inventory);
        }
        public void UpdateSorting()
        {
            if (_inventory != null)
            {
                if (_sortingType == SortingType.None)
                {

                }
                else if (_sortingType == SortingType.Name)
                {
                    SortByName();
                }
                else if (_sortingType == SortingType.Category)
                {
                    SortByCategory();
                }
                else if (_sortingType == SortingType.Weight)
                {
                    SortByWeight();
                }
                else if (_sortingType == SortingType.Count)
                {
                    SortByCount();
                }
            }
        }
        private void SortByName()
        {
            _inventory.ItemList.Sort(delegate (Item x, Item y)
            {
                if (x.Name == null && y.Name == null) return 0;
                else if (x.Name == null) return -1;
                else if (y.Name == null) return 1;
                else return x.Name.CompareTo(y.Name);
            });
        }
        private void SortByCategory()
        {
            _inventory.ItemList.Sort(delegate (Item x, Item y)
            {
                if (x.Category == null && y.Category == null) return 0;
                else if (x.Category == null) return -1;
                else if (y.Category == null) return 1;
                else return x.Category.CompareTo(y.Category);
            });
        }
        private void SortByWeight()
        {
            _inventory.ItemList.Sort(delegate (Item x, Item y)
            {
                if (x.Weight == 0 && y.Weight == 0) return 0;
                else if (x.Weight > y.Weight) return -1;
                else if (x.Weight <= y.Weight) return 1;
                else return x.Weight.CompareTo(y.Weight);
            });
        }
        private void SortByCount()
        {
            _inventory.ItemList.Sort(delegate (Item x, Item y)
            {
                if (x.Count == 0 && y.Count == 0) return 0;
                else if (x.Count > y.Count) return -1;
                else if (x.Count <= y.Count) return 1;
                else return x.Count.CompareTo(y.Count);
            });
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
