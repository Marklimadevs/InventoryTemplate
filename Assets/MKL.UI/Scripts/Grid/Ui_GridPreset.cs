using System.Collections;
using UnityEngine;

namespace MKL.UI
{
    [CreateAssetMenu(fileName = "New Grid Preset",
     menuName = "MKL/UI/New Grid Preset")]
    public class Ui_GridPreset : ScriptableObject
    {
        [Header("UI Config---------------------------------")]
        [Space(1)]
        [SerializeField]
        [Range(1, 5)]
        private float _UiScale = 1;

        [Header("Grid Config--------------------------------")]
        [Space(1)]
        [SerializeField] private float _GridSizeX = 100;
        [SerializeField] private float _GridSizeY = 100;
        [SerializeField] private float _SpacingX = 5;
        [SerializeField] private float _SpacingY = 5;
        [SerializeField] private TextAnchor _childAlignment;
    }
}