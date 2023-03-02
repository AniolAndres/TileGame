
using Assets.Data.Level;
using System;
using System.Collections.Generic;

namespace Assets.Controllers {

	public class GameStateStateMachine {

		readonly List<IGameplayState> gameplayStates = new List<IGameplayState>();

		IGameplayState currentState;

		public event Action<TileData> OnCreateUnitPushRequested;

		public event Action OnPostMovementRequested;

		readonly GameplayContext gameplayContext;

		public GameStateStateMachine(GameplayContext gameplayContext) {
			this.gameplayContext = gameplayContext;
		}

		public void Initialize() {

			currentState = new GameplayStateNothingSelected(gameplayContext);
			currentState.OnGameplayStateChange = ChangeState;
			currentState.OnPushCreateUnitMenuRequested = RequestPushCreateUnit;
			currentState.OnPushPostMovementUnitRequested = RequestPushPostMovementState;
			currentState.OnEnter();
		}

		private void ChangeState(IGameplayState newState) {
			currentState.OnExit();
			currentState = newState;
			currentState.OnGameplayStateChange = ChangeState;
			currentState.OnPushCreateUnitMenuRequested = RequestPushCreateUnit;
			currentState.OnPushPostMovementUnitRequested = RequestPushPostMovementState;
			currentState.OnEnter();
		}

		private void RequestPushPostMovementState() {
			OnPostMovementRequested?.Invoke();
		}

		private void RequestPushCreateUnit(TileData tileData) {
			OnCreateUnitPushRequested?.Invoke(tileData);
		}

		public void OnDestroy() {
			
		}

		public void OnTileClicked(TileData tileData) {
			currentState.OnTileClicked(tileData);
		}
	}
}