using Assets.Catalogs;
using Assets.Controllers;
using Assets.Data;
using Assets.Data.Level;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Controllers {

	public abstract class GenericGameplayState {

		public Action<IGameplayState> OnGameplayStateChange { get; set; }

		public Action<TileData> OnPushCreateUnitMenuRequested { get; set; }

		public Action OnPushPostMovementUnitRequested { get; set; }

		readonly protected GameplayContext gameplayContext;

		protected MapController mapController => gameplayContext.MapController;

		protected UnitHandler unitHandler => gameplayContext.UnitHandler;

		protected BuildingHandler buildingHandler => gameplayContext.BuildingHandler;

		protected WarController warController => gameplayContext.WarController;

		protected TilesCatalog tilesCatalog => gameplayContext.TilesCatalog;

		public GenericGameplayState(GameplayContext gameplayContext) {
			this.gameplayContext = gameplayContext;
		}
	}
}