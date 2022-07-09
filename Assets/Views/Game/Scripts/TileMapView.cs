using System;
using UnityEngine;

namespace Assets.Views {
    public class TileMapView : MonoBehaviour {

        public TileView InstantiateTileView(TileView prefab, float x, float y, float sideLength) {
            var tileView = Instantiate(prefab,  this.transform);
            tileView.transform.AsRectTransform().sizeDelta = new Vector2(sideLength, sideLength);
            tileView.transform.AsRectTransform().anchoredPosition = new Vector2(x, y);          
            return tileView;
        }

        public UnitMapView CreateUnitView(UnitMapView prefab, Vector2 position, float sideLength) {
            var unitView = Instantiate(prefab, this.transform);
            unitView.transform.AsRectTransform().sizeDelta = new Vector2(sideLength, sideLength);
            unitView.transform.AsRectTransform().anchoredPosition = new Vector2(position.x, position.y);
            return unitView;
        }
    }
}