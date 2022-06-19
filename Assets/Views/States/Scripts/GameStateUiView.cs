using Assets.Views;
using Assets.Views.Game;
using UnityEngine;

namespace Assets.Views {
    public class GameStateUiView : UiView {
        [SerializeField]
        private TileMapView tileMapView;

        [SerializeField]
        private CameraView cameraView;

        public TileMapView TileMapView => tileMapView;

        public CameraView CameraView => cameraView;

    }

}