using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;


public class SecondStateUiView : UiView
{
    [SerializeField]
    private Button popButton;

    public event Action OnPopRequested;

    private void OnEnable() {
        popButton.onClick.AddListener(FirePopAction);
    }

    private void OnDisable() {
        popButton.onClick.RemoveListener(FirePopAction);
    }

    public void FirePopAction() {
        OnPopRequested?.Invoke();
    }
}
