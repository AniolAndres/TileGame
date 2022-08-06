
using Assets.Data;
using Assets.Views;
using System;
using UnityEngine;

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

        public void OnDeselect() {
            unitView.SetSelectStatus(false);
        }

        public void OnMove(Vector2 newPosition) {
            unitView.MoveUnitViewTo(newPosition);
        }

        public void OnDestroy() {
            throw new NotImplementedException();
        }
    }
}