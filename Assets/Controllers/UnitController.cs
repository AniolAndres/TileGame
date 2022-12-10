
using Assets.Data;
using Assets.Views;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controllers {
    public class UnitController {

        private readonly UnitMapView unitView;

        private readonly UnitModel unitModel;

        public event Action OnMovementStart;

        public event Action OnMovementEnd;

        public UnitController(UnitMapView unitView, UnitModel unitModel) {
            this.unitView = unitView;
            this.unitModel = unitModel;
        }

        public string GetUnitId() {
            return unitModel.GetId();
        }

        public void FireMovementStartAction() {
            OnMovementStart?.Invoke();  
        }

        public void FireMovementEndAction() {
            OnMovementEnd?.Invoke();
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

        public void OnMove(Vector2Int firstGridPosition, List<Vector2Int> gridPositions, List<Vector2> pathPositions) {
            unitView.MoveUnitViewTo(firstGridPosition, gridPositions, pathPositions);
            unitModel.SetToAlreadyMoved();
        }

        public void Refresh()
        {
            unitModel.RefreshUnit();
        }

        public bool CanMove()
        {
            return unitModel.CanMove;
        }

        public void OnCreate() {
            unitView.OnMovementEnd += FireMovementEndAction;
            unitView.OnMovementStart += FireMovementStartAction;
        }

        public void OnDestroy() {
            unitView.OnMovementEnd -= FireMovementEndAction;
            unitView.OnMovementStart -= FireMovementStartAction;
        }
    }
}