using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class UI_WindowMenuControl : UIBehaviour
{
    [SerializeField] private RectTransform _RectTransform;
    [SerializeField] private RectTransform _content;
    [SerializeField] private ScrollRect _ScrolRect;
    [SerializeField] private Vector2 _screenSize = new Vector2();
    // public Vector2 ScreenSize
    // {
    //     get => new Vector2(Screen.height, Screen.height);
    //     get => new Vector2(_screenSize.x, _screenSize.y);
    // }
    public Vector2 WindowsSize
    {
        get
        {
            if (_content.sizeDelta.y >= _screenSize.y)
            {                
                return new Vector2(_content.sizeDelta.x, _screenSize.y);
            }
            else
            {
                return _content.sizeDelta;
            }
        }
    }
    #if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        GetReferences();
        SetSize();
    }
    #endif
    protected override void Start()
    {
        base.Start();
        GetReferences();
    }
    private void FixedUpdate()
    {
        SetSize();
    }
    private void GetReferences()
    {
        if (_ScrolRect == null)
        {
            _ScrolRect = GetComponent<ScrollRect>();
        }
        if (_RectTransform != null)
        {
            _RectTransform = GetComponent<RectTransform>();
        }
    }
    private void SetSize()
    {
        _RectTransform.sizeDelta = WindowsSize;        
    }
}
