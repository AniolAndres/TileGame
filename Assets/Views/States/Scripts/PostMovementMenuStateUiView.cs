using Assets.ScreenMachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Views.States.Scripts {
    public class PostMovementMenuStateUiView : UiView {

        [SerializeField]
        private Button confirmButton;
        
        public event Action OnPopRequested;

        public event Action OnConfirm;

        public override void OnUpdate() {
            if (Input.GetMouseButtonDown(1)) {
                OnPopRequested?.Invoke();
            }
        }

        private void OnEnable() {
            confirmButton.onClick.AddListener(FireConfirmEvent);
        }

        private void OnDisable() {
            confirmButton.onClick.RemoveListener(FireConfirmEvent);
        }

        private void FireConfirmEvent() {
            OnConfirm?.Invoke();
        }
    }
}