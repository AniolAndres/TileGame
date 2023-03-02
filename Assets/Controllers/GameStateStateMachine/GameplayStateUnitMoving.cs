using Assets.Data.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controllers {
	public class GameplayStateUnitMoving : GenericGameplayState, IGameplayState {

		readonly private List<Vector2Int> gridPath;

		public GameplayStateUnitMoving(GameplayContext gameplayContext, List<Vector2Int> gridPath) : base(gameplayContext) {

			this.gridPath = gridPath;
		}

		public void OnEnter() {
			unitHandler.OnMovementStop += OnMovementStop;

			var listOfRealPositions = mapController.GetListOfRealPositions(gridPath);
			unitHandler.MoveSelectedUnit(gridPath, listOfRealPositions);
		}

		private void OnMovementStop() {
			OnPushPostMovementUnitRequested?.Invoke();
		}

		public void OnExit() {
			unitHandler.OnMovementStop -= OnMovementStop;
		}

		public void OnTileClicked(TileData tileData) {
			//---
		}
	}
}