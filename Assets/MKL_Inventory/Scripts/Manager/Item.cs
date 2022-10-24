using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKL.Inventory
{
    /// <summary>
    /// Representacao de um Item para o Inventario
    /// </summary>
    [System.Serializable]
    public class Item
    {
        public Item(ItemBase itemBase, int count, int bagId, int ItemId)
        {
            if (itemBase != null)
            {
                this._itemBase = itemBase;
            }
            this._count = count;
            this._bagId = bagId;
            this._ItemId = ItemId;
            LoadItem();
        }

        //Variaveis Privates
        #region Campos 

        [Space(1)]
        [Header("-----Geral Config-----")]
        [SerializeField] private string _itemName;
        [SerializeField] private string _itemCategory;
        [SerializeField] private ItemBase _itemBase;
        [SerializeField] private int _count = 100;
        [SerializeField] private float _weight = 0f;
        [SerializeField] private float _weightMax = 0f;
        [SerializeField] private bool _IsStackable = true;
        [SerializeField] private Sprite _uiIcon;

        [Space(1)]
        [Header("----Durability Config----")]
        [SerializeField] private bool _enableDurability = false;
        [SerializeField] private int _durabilityValue = 100;
        [SerializeField] private float _durabilityPorcent;
        [SerializeField] private int _durabilityMax = 100;

        [Space(1)]
        [Header("----Internal Ids----")]
        [SerializeField] private int _bagId;
        [SerializeField] private int _ItemId;

        #endregion

        //Variaveis Publicas
        #region Propriedades

        //ItemBase Config
        public string Name
        {
            get => _itemName;
        }
        public string Category
        {
            get => _itemCategory;
        }
        public bool IsStackable
        {
            get => _IsStackable;
        }
        public float Weight
        {
            get => _weight;
        }
        public float WeightMax
        {
            get => _weight * Count;
        }
        public Sprite UiIcon
        {
            get => _uiIcon;
        }
        public bool EnableDurability
        {
            get => _enableDurability;
        }
        public float DurabilityMax
        {
            get => _durabilityMax;
        }
        public float DurabilityValue
        {
            get => _durabilityValue;
        }
        public float DurabilityPorcent
        {
            get
            {
                _durabilityPorcent = _durabilityValue / _durabilityMax;
                return _durabilityPorcent;
            }
        }
        //-------------------------------------
        public ItemBase itemBase
        {
            get => _itemBase;
        }
        public int Count
        {
            get => _count;
        }
        public bool IsEmply
        {
            get => _count <= 0;
        }
        public int BagId
        {
            get => _bagId;
        }

        #endregion

        //Funcoes
        #region Metodos      
        public void LoadItem()
        {
            if (_itemBase != null)
            {
                _itemName = _itemBase.Name;
                _weight = _itemBase.Weight;
                _IsStackable = _itemBase.IsStackable;
                _uiIcon = _itemBase.UiIcon;
                _itemCategory = _itemBase.ItemCategory;
                _enableDurability = _itemBase.EnableDurability;
                _durabilityValue = _itemBase.DurabilityValue;
                _durabilityPorcent = _itemBase.DurabilityPorcent;
                _durabilityMax = _itemBase.DurabilityMax;
            }
        }
        public void Add(int value)
        {
            _count += value;
        }
        public void Remove(int value)
        {
            _count -= value;
        }

        //UPDATES----------------------------------------
        public void UpdateBagId(int value)
        {
            _bagId = value;
        }
        public void UpdateItemId(int value)
        {
            _ItemId = value;
        }

        #endregion
    }
}
