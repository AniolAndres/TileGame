using UnityEngine;
using System.Collections.Generic;
using Assets.Views;

namespace Assets.Controllers {
    public class PlayerController {

        private readonly PlayerView playerView;

        private readonly List<UnitController> controlledUnits = new List<UnitController>();

        private readonly List<BuildingTileController> controlledBuildings = new List<BuildingTileController>();

        private readonly FundsController fundsController;

        public PlayerController(PlayerView playerView, FundsController fundsController) {
            this.fundsController = fundsController;
            this.playerView = playerView;
        }

        public void RemoveUnit(UnitController unit) {
            controlledUnits.Remove(unit);
        }

        public void AddUnit(UnitController unit) {
            controlledUnits.Add(unit);
        }

        public void OnTurnStart() {
            fundsController.GainFunds(controlledBuildings.Count * 1000);
        }

        public void OnTurnEnd() {

        }

    }
}