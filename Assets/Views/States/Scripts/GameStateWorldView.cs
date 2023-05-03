using Assets.ScreenMachine;
using Assets.Views.Game;
using System;
using UnityEngine;

namespace Assets.Views {
    public class GameStateWorldView : WorldView {

        [SerializeField]
        private TileMapView tileMapView;

        [SerializeField]
        private CameraView cameraView;

        public TileMapView TileMapView => tileMapView;

        public CameraView CameraView => cameraView;

		public event Action OnSecondaryButtonClick;

		public event Action OnMouseUpdate;

		public override void OnUpdate() {
            base.OnUpdate();

            cameraView.OnUpdate();

			OnMouseUpdate?.Invoke();

			if (Input.GetMouseButtonDown(1)) {
				OnSecondaryButtonClick?.Invoke();
			}
		}
    }

}