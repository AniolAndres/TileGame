
using Assets.Views;
using Assets.Data;
using System;

namespace Assets.Controllers {
    public class PurchaseTileController {

        private readonly UnitPurchaseView view;

        private readonly UnitPurchaseModel model;

        public event Action<string> OnUnitBought;

        public PurchaseTileController(UnitPurchaseView view, UnitPurchaseModel model) {
            this.view = view;
            this.model = model;
        }

        public void OnCreate() {
            view.OnClickView += OnViewClicked;
        }

        public void OnDestroy() {
            view.OnClickView -= OnViewClicked;
        }

        private void OnViewClicked() {
            var hasEnoughFunds = model.CanPay();
            if (!hasEnoughFunds) {
                return;
            }

            var unitId = model.GetUnitId();
            OnUnitBought?.Invoke(unitId);
        }
    }
}