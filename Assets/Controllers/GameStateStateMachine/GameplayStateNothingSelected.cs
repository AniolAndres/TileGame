using Assets.Data;
using Assets.Data.Level;
using Assets.ScreenMachine;
using System;

namespace Assets.Controllers {
	public class GameplayStateNothingSelected : GenericGameplayState, IGameplayState {

		public GameplayStateNothingSelected(GameplayContext gameplayContext) : base(gameplayContext) {
		}

		public void OnEnter() {
			
		}

		public void OnExit() {
			
		}

		public void OnTileClicked(TileData tileData) {

			var currentArmy = warController.GetCurrentTurnArmyIndex();
			var isSpaceEmpty = unitHandler.IsSpaceEmpty(tileData.Position);
			if (!isSpaceEmpty) {
				var isUnitFromPlayer = unitHandler.IsFromArmy(tileData.Position, currentArmy);
				var canUnitMove = unitHandler.GetUnitControllerAtPosition(tileData.Position).CanMove();

				if(isUnitFromPlayer && canUnitMove) {
					OnGameplayStateChange?.Invoke(new GameplayStateUnitSelected(gameplayContext, tileData));
				}

				return;
			}

			var tileId = mapController.GetTypeFromTile(tileData.Position);
			var tileEntry = tilesCatalog.GetEntry(tileId);
			var isBuilding = tileEntry.CanCreate;

			if (isBuilding) {
				var isBuildingFromPlayer = buildingHandler.IsBuildingFromPlayer(currentArmy, tileData.Position);

				if (isBuildingFromPlayer) {
					OnPushCreateUnitMenuRequested?.Invoke(tileData);
				}
			}

		}


	}
}