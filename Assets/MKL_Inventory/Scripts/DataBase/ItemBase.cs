using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKL.Inventory
{
    [CreateAssetMenu(fileName = "New Item",
    menuName = "MKL/Inventory/New Item")]
    public class ItemBase : ScriptableObject
    {
        [Space(1)]
        [Header("-----Geral Config-----")]
        [SerializeField] private string _itemName;        
        [SerializeField] private string _itemCategory;
        [SerializeField] private float _weight = 0f;
        [SerializeField] private bool _IsStackable = true;
        [SerializeField] private Sprite _UiIcon;

        [Space(1)]
        [Header("----Durability Config----")]
        [SerializeField] private bool _enableDurability = false;
        [SerializeField] private int _durabilityValue = 100;
        [SerializeField] private int _durabilityMax = 100;
        public string Name
        {
            get => _itemName != null ? _itemName : "";
        }
        public string ItemCategory
        {
            get => _itemCategory != null ? _itemCategory : "";
        }
        public float Weight
        {
            get => _weight;
        }
        public float DurabilityPorcent
        {
            get => Mathf.Clamp((float)_durabilityValue / (float)_durabilityMax, 0f, 1f);
        }
        public bool HasDurability
        {
            get => _enableDurability;
        }
        public bool IsStackable
        {
            get => _IsStackable;
        }
        public Sprite UiIcon
        {
            get => _UiIcon;
        }

        #region  Durabilition
        public bool EnableDurability
        {
            get => _enableDurability;
        }
        public int DurabilityMax
        {
            get => _durabilityMax;
        }
        public int DurabilityValue
        {
            get => _durabilityValue;
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
