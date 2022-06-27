using UnityEngine;
using System.Collections;
using System;

namespace Assets.Views.States.Scripts {
    public class PopupStateUiView : UiView {

        [SerializeField]
        private Transform parentPurchase;

        public event Action OnPopRequested;

        private void Update() {
            if (Input.GetMouseButton(1)) {
                OnPopRequested?.Invoke();
            }
        }

        public UnitPurchaseView InstantiatePurchaseView(UnitPurchaseView prefab, UnitPurchaseViewData unitPurchaseViewData) {
            var view = Instantiate(prefab, parentPurchase);
            view.Setup(unitPurchaseViewData);
            return view;
        }
    }
}