
using Assets.Views;
using Assets.Data;
using System;

namespace Assets.Controllers {
    public class PurchaseTileController {

        private readonly UnitPurchaseView view;

        private readonly UnitPurchaseModel model;

        public event Action<string> OnUnitBought;

        public event Action<string> OnUnitHovered;

        public PurchaseTileController(UnitPurchaseView view, UnitPurchaseModel model) {
            this.view = view;
            this.model = model;
        }

        public void OnCreate() {
            view.OnClickView += OnViewClicked;
            view.OnHover += OnViewHovered;
        }

        public void OnDestroy() {
            view.OnClickView -= OnViewClicked;
            view.OnHover -= OnViewHovered;
        }

        private void OnViewHovered() {
            var unitId = model.GetUnitId();
            OnUnitHovered?.Invoke(unitId);
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