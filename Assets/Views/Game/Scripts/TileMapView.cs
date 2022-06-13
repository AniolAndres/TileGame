using UnityEngine;
using System.Collections;
using System;

namespace Assets.Views {
    public class TileMapView : MonoBehaviour {

        [SerializeField]
        private TileView grassPrefab;

        [SerializeField]
        private TileView waterPrefab;


        public TileView InstantiateTileView(int i, int j) {
            Debug.Log($"Creating tile at {i} {j}");
            return new TileView();
        }
    }
}