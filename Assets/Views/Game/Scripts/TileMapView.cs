using UnityEngine;
using System.Collections;
using System;

namespace Assets.Views {
    public class TileMapView : MonoBehaviour {

        [SerializeField]
        private TileView grassPrefab;

        [SerializeField]
        private TileView waterPrefab;

        public TileView InstantiateTileView(float x, float y) {
            var tileView = Instantiate(waterPrefab,  this.transform);
            tileView.transform.AsRectTransform().anchoredPosition = new Vector2(x, y);
            return tileView;
        }

    }
}