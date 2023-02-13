using UnityEngine;
using System.Collections.Generic;
using Assets.Views;
using Assets.Data;
using System;
using Assets.Catalogs.Scripts;

namespace Assets.Controllers {
    public class PlayerController {

        private readonly PlayerView playerView;

        private readonly PlayerModel playerModel;

        private readonly List<UnitController> controlledUnits = new List<UnitController>();

        private readonly FundsController fundsController;

        public void Hide(bool instant) {
            playerView.Hide(instant);
        }

        public void Show(bool instant) {
            playerView.Show(instant);
        }

        public PlayerController(PlayerView playerView, PlayerModel model, FundsController fundsController) {
            this.fundsController = fundsController;
            this.playerModel = model;
            this.playerView = playerView;
        }

        public void RemoveUnit(UnitController unit) {
            controlledUnits.Remove(unit);
        }

        public void AddUnit(UnitController unit) {
            controlledUnits.Add(unit);
        }

        public void OnTurnStart() {
            //fundsController.GainFunds(controlledBuildings.Count * 1000);
            fundsController.GainFunds(2 * 1000);
            playerView.Show(false);
            playerView.UpdateCount(fundsController.CurrentFunds);
        }

        public void OnTurnEnd() {
            playerView.Hide(false);
        }

        public void OnCreate() {
            var uiData = playerModel.GetCommanderViewData();
            playerView.Setup(ref uiData);
        }

        public void OnDestroy() {
            
        }

        public int GetArmyIndex()
        {
            return playerModel.GetArmyIndex();
        }

        public int GetCurrentFunds()
        {
            return fundsController.CurrentFunds;
        }

        public string GetArmyColorId() {
            return playerModel.GetColorId();
        }

        public void TakeFundsFromPlayer(int cost)
        {
            fundsController.SpendFunds(cost);
            playerView.UpdateCount(fundsController.CurrentFunds);
        }

        public string GetArmyCommanderId() {
            return playerModel.GetArmyCommanderId();
        }
    }
}