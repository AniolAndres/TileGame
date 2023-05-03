using Assets.ScreenMachine;
using Assets.Views;
using Assets.Views.Game;
using System;
using UnityEngine;

namespace Assets.Views {
    public class GameStateUiView : UiView {

        [SerializeField]
        private PlayerView playerViewPrefab;

        [SerializeField]
        private Transform playersParent;

        public PlayerView InstantiatePlayerView() {
            var playerView = Instantiate(playerViewPrefab, playersParent);
            return playerView;
        }
    }

}