
using Assets.Data;
using Assets.Views;
using System;

namespace Assets.Controllers {
    public class UnitController {

        private readonly UnitMapView unitView;

        private readonly UnitModel unitModel;

        public UnitController(UnitMapView unitView, UnitModel unitModel) {
            this.unitView = unitView;
            this.unitModel = unitModel;
        }

        public string GetUnitId() {
            return unitModel.GetId();
        }

        public void OnSelect() {
            unitView.SetSelectStatus(true);
        }
    }
}