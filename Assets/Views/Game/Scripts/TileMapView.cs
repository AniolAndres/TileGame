using System;
using UnityEngine;

namespace Assets.Views {
    public class TileMapView : MonoBehaviour {

        [SerializeField]
        private TileView tileViewPrefab;

        public TileView InstantiateTileView(Color tileColor, float x, float y, float sideLength) {
            var tileView = Instantiate(tileViewPrefab,  this.transform);
            tileView.transform.AsRectTransform().sizeDelta = new Vector2(sideLength, sideLength);
            tileView.transform.AsRectTransform().anchoredPosition = new Vector2(x, y);
            tileView.Setup(tileColor);
            return tileView;
        }

        public UnitMapView CreateUnitView(UnitMapView prefab, Sprite unitSprite, Vector2 position, float sideLength) {
            var unitView = Instantiate(prefab, this.transform);
            unitView.transform.AsRectTransform().sizeDelta = new Vector2(sideLength, sideLength);
            unitView.transform.AsRectTransform().anchoredPosition = new Vector2(position.x, position.y);
            unitView.SetViewData(unitSprite);
            return unitView;
        }
    }
}