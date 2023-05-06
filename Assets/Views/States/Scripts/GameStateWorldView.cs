using Assets.ScreenMachine;
using Assets.Views.Game;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Views {
    public class GameStateWorldView : WorldView {

        [SerializeField]
        private TileMapView tileMapView;

        [SerializeField]
        private CameraView cameraView;

        [SerializeField]
        private Button mapButton;

        public TileMapView TileMapView => tileMapView;

        public CameraView CameraView => cameraView;

		public event Action OnSecondaryButtonClick;

		public event Action OnMouseUpdate;

		private void OnEnable() {
			mapButton.onClick.AddListener(FireMapClickEvent);
		}

		private void OnDisable() {
			mapButton.onClick.RemoveListener(FireMapClickEvent);
		}

		private void FireMapClickEvent() {
			tileMapView.FireMapClickEvent();
		}


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