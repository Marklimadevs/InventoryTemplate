using UnityEngine.EventSystems;
using UnityEngine.UI;
using MKL.Inventory;
using MKL.Utilities;
using UnityEngine;
using TMPro;

namespace MKL.InventoryUI
{
    /// <summary>
    /// Representacao de um Item na UI
    /// </summary>
    public class UI_Item : MonoBehaviour, IPointerClickHandler
    {
        [Header("Item Config ----------------------------")]
        [Space(1)]
        [SerializeField] private Item _item;
        [Header("UI Config ----------------------------")]
        [Space(1)]
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _txtName;
        [SerializeField] private TextMeshProUGUI _txtCategory;
        [SerializeField] private TextMeshProUGUI _txtWeight;
        [SerializeField] private TextMeshProUGUI _txtCount;

        [Header("Durability Config -----------------------")]
        [Space(1)]
        [SerializeField] private Transform _durabilityGroup;
        [SerializeField] private Image _durabilityImg;
        [SerializeField] private Gradient _durabilityColor;

        #region  Updates------------------------------------------------   
        public void Updateitem(Item _NewItem, int _ItemId = 0)
        {
            _item = new Item(itemBase: _NewItem.itemBase, count: _NewItem.Count, bagId: _NewItem.BagId, ItemId: _ItemId);
            UpdateUi();
            UpdateDurability();
        }
        private void UpdateUi()
        {
            if (_txtName != null)
            {
                _txtName.SetText(_item.Name);
            }
            if (_txtCategory != null)
            {
                _txtCategory.SetText(_item.Category);
            }
            if (_txtCount != null)
            {
                if (_item.Count > 1)
                {
                    _txtCount.gameObject.SetActive(true);
                    _txtCount.SetText(ConvertFormats.ReturnUnValue(_item.Count));
                }
                else
                {
                    _txtCount.gameObject.SetActive(false);
                }
            }
            if (_txtWeight != null)
            {
                _txtWeight.SetText(ConvertFormats.ReturnKgValue(_item.WeightMax));
            }
            if (_item.UiIcon != null)
            {
                _icon.sprite = _item.UiIcon;
            }
        }
        private void UpdateDurability()
        {
            if (_durabilityGroup != null)
            {
                _durabilityGroup.gameObject.SetActive(_item.EnableDurability);

                if (_item.EnableDurability && _durabilityImg != null)
                {
                    _durabilityImg.fillAmount = _item.DurabilityPorcent;
                    _durabilityImg.color = _durabilityColor.Evaluate(_item.DurabilityPorcent);
                }
            }

        }
        #endregion

        #region  Inputs------------------------------------------------  
        public void LeftClick()
        {
            UI_ItemMenu _itemMenu = GameObject.FindObjectOfType<UI_ItemMenu>();
            if (_itemMenu != null)
            {
                //Debug.Log("_itemMenu != null");
                _itemMenu.Close();
            }
            else
            {
                //Debug.Log("_itemMenu == null");
            }
            UI_InventoryManager UiInvManager = GameObject.FindObjectOfType<UI_InventoryManager>();
            if (UiInvManager != null)
            {
                //Debug.Log("_itemMenu != null");
                UiInvManager.TransfenderItem(this._item, Count: 1);
            }
            else
            {
                //Debug.Log("_itemMenu == null");
            }
        }
        public void RightClick()
        {
            UI_ItemMenu _itemMenu = GameObject.FindObjectOfType<UI_ItemMenu>();
            if (_itemMenu != null)
            {
                //Debug.Log("_itemMenu != null");
                _itemMenu.Open(_item, Position: Input.mousePosition);
            }
            else
            {
                //Debug.Log("_itemMenu == null");
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                LeftClick();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                RightClick();
            }
            else if (eventData.button == PointerEventData.InputButton.Middle)
            {
                //Debug.Log("InputButton.Middle");
            }
        }
        #endregion

        #region Editor --------------------------------------

#if UNITY_EDITOR

        private void OnValidate()
        {
            UpdateDurability();
        }

#endif
        #endregion
    }

}
