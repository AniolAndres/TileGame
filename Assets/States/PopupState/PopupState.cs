
using Assets.Controllers;
using Assets.Data;
using Assets.Data.Models;
using Assets.Views;
using Assets.Views.States.Scripts;
using System.Collections.Generic;

namespace Assets.States {
    public class PopupState : BaseState<PopupStateUiView, PopupStateWorldView>, IStateBase {

        private const string Id = "PopupState";

        private PopupStateModel model;

        private List<PurchaseTileController> tileControllerList = new List<PurchaseTileController>();

        public PopupState(Context context) : base(context) { }

        public string GetId() {
            return Id;
        }

        public void OnBringToFront() {

        }

        public void OnCreate() {
            uiView.OnPopRequested += PopState;

            model = new PopupStateModel(context.catalogs.UnitsCatalog);

            CreatePurchaseControllers();
        }

        private void CreatePurchaseControllers() {

            var units = model.GetUnits();
            
            foreach(var unit in units) {
                var unitPurchaseView = uiView.InstantiatePurchaseView(unit.UnitPurchaseView, new UnitPurchaseViewData());
                var unitPurchaseModel = new UnitPurchaseModel();
                var unitController = new PurchaseTileController(unitPurchaseView, unitPurchaseModel);
                tileControllerList.Add(unitController);
            }
        }

        public void OnDestroy() {
            uiView.OnPopRequested -= PopState;
        }

        public void OnSendToBack() {

        }

    }

}