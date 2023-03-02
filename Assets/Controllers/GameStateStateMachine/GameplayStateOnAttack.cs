using Assets.Data.Level;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Controllers {
	public class GameplayStateOnAttack : GenericGameplayState, IGameplayState {
		public GameplayStateOnAttack(GameplayContext gameplayContext) : base(gameplayContext) {
		}

		public event Action<IGameplayState> OnGameplayStateChange;

		public void OnEnter() {
			
		}

		public void OnExit() {
			
		}

		public void OnTileClicked(TileData tileData) {
			
		}
	}
}