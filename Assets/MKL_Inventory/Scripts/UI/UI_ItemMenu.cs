using UnityEngine;
using UnityEngine.UI;
using MKL.Inventory;
using TMPro;

namespace MKL.InventoryUI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UI_ItemMenu : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private int _value;
        [SerializeField] private TextMeshProUGUI _txtItemName;
        [SerializeField] private TextMeshProUGUI _txtItemkg;
        [SerializeField] private TextMeshProUGUI _txtItemInputQuant;
        [SerializeField] private TMP_InputField _inputFieldQuant;
        [SerializeField] private Button _transFendeButton;
        [SerializeField] private Slider _inputSlideQuant;
        private CanvasGroup _canvas;
        private void Start()
        {
            _canvas = GetComponent<CanvasGroup>();
        }

        #region  External
        public void Open(Item Item, Vector2 Position = new Vector2())
        {
            _canvas.alpha = 1;
            _canvas.blocksRaycasts = true;
            _canvas.interactable = true;
            _item = Item;
            _inputSlideQuant.maxValue = _item.Count;
            UpdateInputQuant(_item.Count);
            UpdateSlideQuant(_inputFieldQuant.text);
            if (Position != null)
            {
                ChooseSideUi(Position);
            }
        }
        public void Close()
        {
            //_item = null;
            _value = 0;
            _canvas.alpha = 0;
            _canvas.blocksRaycasts = false;
            _canvas.interactable = false;
        }
        public void TransfenderItem()
        {
            GameObject.FindObjectOfType<UI_InventoryManager>().TransfenderItem(_item, _value);
            Close();
        }
        public void UpdateInputQuant(float Value)
        {
            if (_inputFieldQuant != null)
            {
                _value = (int)Value;
                _inputFieldQuant.text = _value.ToString("N0");
            }
            UpdateInfo();
        }
        public void UpdateSlideQuant(string Value)
        {
            if (_inputSlideQuant != null)
            {
                _inputSlideQuant.value = float.Parse(Value);
                _value = (int)_inputSlideQuant.value;
            }
            UpdateInfo();
        }
        #endregion

        #region  internal
        private void UpdateInfo()
        {
            if (_item != null)
            {
                if (_txtItemInputQuant != null)
                {
                    _txtItemInputQuant.SetText(_item.Count.ToString());
                }
                if (_txtItemkg != null &&
                _inputSlideQuant != null)
                {
                    _txtItemkg.SetText((_inputSlideQuant.value * _item.Weight).ToString("N2") + "KG");
                }
                if (_txtItemName != null)
                {
                    _txtItemName.SetText(_item.Name);
                }
            }
        }
        private void ChooseOptions()
        {

        }
        private void ChooseSideUi(Vector2 Position)
        {
            RectTransform ThisRect = GetComponent<RectTransform>();

            Canvas canvas = FindObjectOfType<Canvas>();

            float h = canvas.GetComponent<RectTransform>().rect.height;
            float w = canvas.GetComponent<RectTransform>().rect.width;

            if (Position.x > w / 2)
            {
                //Esquerda
                ThisRect.pivot = new Vector2(1, ThisRect.pivot.y);
            }
            else
            {
                //Direita
                ThisRect.pivot = new Vector2(0, ThisRect.pivot.y);
            }

            if (Position.y > h / 2)
            {
                //Cima
                ThisRect.pivot = new Vector2(ThisRect.pivot.x, 1);
            }
            else
            {
                //Baixo
                ThisRect.pivot = new Vector2(ThisRect.pivot.x, 0);
            }
            ThisRect.localPosition = Position - new Vector2(w / 2, h / 2);

        }

        #endregion
    }
}
