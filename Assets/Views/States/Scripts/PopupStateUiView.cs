using UnityEngine;
using System.Collections;
using System;

namespace Assets.Views.States.Scripts {
    public class PopupStateUiView : UiView {

        public event Action OnPopRequested;

        private void Update() {
            if (Input.GetMouseButton(1)) {
                OnPopRequested?.Invoke();
            }
        }
    }
}