using Assets.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Views {
    public class GameStateUiView : UiView {
        [SerializeField]
        private TileMapView tileMapView;

        public TileMapView TileMapView => tileMapView;
    }

}