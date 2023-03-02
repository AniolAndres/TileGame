
using Assets.Data.Level;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controllers {
	public class GameplayStateUnitSelected : GenericGameplayState, IGameplayState {
		
		readonly TileData tileData;
		
		public GameplayStateUnitSelected(GameplayContext gameplayContext, TileData tileData) : base(gameplayContext) {
			this.tileData = tileData;
		}


		public void OnEnter() {
			//Highlight the tiles
			var currentArmyId = warController.GetCurrentTurnArmyIndex();
			unitHandler.SetUnitSelected(tileData.Position);
			mapController.HighlightAvailableTiles(tileData.Position, currentArmyId, unitHandler.GetSelectedUnitId());
		}

		public void OnExit() {
			//remove the highlight from the tiles
			mapController.ClearCurrentPathfinding();
			unitHandler.ExhaustCurrentUnit();
			unitHandler.DeselectSelectedUnit();
		}

		public void OnTileClicked(TileData tileData) {

			var gridPath = mapController.GetPath(tileData.Position);

			if (gridPath == null) {
				return;
			}

			var unitMovingState = new GameplayStateUnitMoving(gameplayContext, gridPath);

			OnGameplayStateChange?.Invoke(unitMovingState);
		}
	}
}