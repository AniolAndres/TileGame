using Assets.Views.Game;
using UnityEngine;

namespace Assets.Views {
    public class GameStateWorldView : WorldView {

        [SerializeField]
        private TileMapView tileMapView;

        [SerializeField]
        private CameraView cameraView;

        public TileMapView TileMapView => tileMapView;

        public CameraView CameraView => cameraView;

        public override void OnUpdate() {
            base.OnUpdate();

            cameraView.OnUpdate();
        }
    }

}