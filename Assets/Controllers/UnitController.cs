
using Assets.Data;
using Assets.Views;
using System;
using System.Collections.Generic;
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

        public int GetUnitArmyId()
        {
            return unitModel.GetArmyIndex();
        }

        public void OnSelect() {
            unitView.SetSelectStatus(true);
        }

        public void OnDeselect() {
            unitView.SetSelectStatus(false);
        }

        public void OnMove(List<Vector2> pathPositions) {
            unitView.MoveUnitViewTo(pathPositions);
            unitModel.SetToAlreadyMoved();
        }

        public void OnDestroy() {
        }

        public void Refresh()
        {
            unitModel.RefreshUnit();
        }

        public bool CanMove()
        {
            return unitModel.CanMove;
        }
    }
}