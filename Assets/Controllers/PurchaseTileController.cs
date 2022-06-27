
using Assets.Views;
using Assets.Data;

namespace Assets.Controllers {
    public class PurchaseTileController {

        private readonly UnitPurchaseView view;

        private readonly UnitPurchaseModel model;

        public PurchaseTileController(UnitPurchaseView view, UnitPurchaseModel model) {
            this.view = view;
            this.model = model;
        }

    }
}