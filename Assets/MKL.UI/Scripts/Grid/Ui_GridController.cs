using UnityEngine;
using UnityEngine.UI;

namespace MKL.UI
{
    public class Ui_GridController : MonoBehaviour
    {
        [SerializeField] private Ui_GridPreset _GridPreset;
        [SerializeField] private GridLayoutGroup _GridLayoutGroup;
        [SerializeField] private ScrollRect _ScrollRect;

        public void UpdateUiSize(Vector2 _GridSize, Vector2 _Spacing, float _UiScale = 1, TextAnchor _childAlignment = TextAnchor.MiddleCenter)
        {
            UpdateUIGridSize(_GridSize, _UiScale);
            UpdateUISpacing(_Spacing, _UiScale);
            UpdateUIChildAlignment(_childAlignment);
        }
        private void UpdateUIGridSize(Vector2 _GridSize, float _UiScale)
        {
            if (_GridLayoutGroup != null)
            {
                _GridLayoutGroup.cellSize = new Vector2(_GridSize.x * _UiScale, _GridSize.y * _UiScale);
            }
        }
        private void UpdateUISpacing(Vector2 _Spacing, float _UiScale)
        {
            if (_GridLayoutGroup != null)
            {
                _GridLayoutGroup.spacing = new Vector2(_Spacing.x * _UiScale, _Spacing.y * _UiScale);
            }
        }
        private void UpdateUIChildAlignment(TextAnchor _childAlignment)
        {
            if (_GridLayoutGroup != null)
            {
                _GridLayoutGroup.childAlignment = _childAlignment;
            }
        }
        private void UpdateUiScrollRect(bool Horizontal = false, bool Vertical = true, bool Ambos = false)
        {
            if (_ScrollRect != null)
            {
                _ScrollRect.horizontal = (Horizontal || Ambos);
                _ScrollRect.vertical = (Vertical || Ambos);
            }
        }

    }
}