using Assets.Data;
using Assets.Data.Level;
using System;

namespace Assets.Controllers {


	public interface IGameplayState {

		Action<IGameplayState> OnGameplayStateChange { get; set; }

		Action<TileData> OnPushCreateUnitMenuRequested { get; set; }

		Action OnPushPostMovementUnitRequested { get; set; }

		void OnEnter();

		void OnExit();

		void OnTileClicked(TileData tileData);

	}
}