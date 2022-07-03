using UnityEngine;
using System;

namespace Assets.Views {
    public class PopupStateUiView : UiView {

        [SerializeField]
        private Transform parentPurchase;

        [SerializeField]
        private PurchaseInfoView infoView;

        public event Action OnPopRequested;

        private void Update() {
            if (Input.GetMouseButton(1)) {
                OnPopRequested?.Invoke();
            }
        }

        public UnitPurchaseView InstantiatePurchaseView(UnitPurchaseView prefab, ref UnitPurchaseViewData unitPurchaseViewData) {
            var view = Instantiate(prefab, parentPurchase);
            view.Setup(ref unitPurchaseViewData);
            return view;
        }

        public void RefreshInfoPanel(ref PurchaseInfoViewData infoData) {
            infoView.SetUp(ref infoData);
        }
    }
}