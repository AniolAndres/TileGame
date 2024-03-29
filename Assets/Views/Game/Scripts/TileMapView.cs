﻿using Assets.Views.Game;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Views {
    public class TileMapView : MonoBehaviour {

        [SerializeField]
        private TileView tileViewPrefab;

        [SerializeField]
        private Transform tilesParent;

        [SerializeField]
        private TileCursorView tileCursorView;

        public TileCursorView TileCursorView => tileCursorView;

        public event Action OnMapClicked;


        public void FireMapClickEvent() {
            OnMapClicked?.Invoke();
        }

        public TileView InstantiateTileView(ref TileViewData tileViewData) {
            var tileView = Instantiate(tileViewPrefab, tilesParent);
            tileView.transform.AsRectTransform().sizeDelta = new Vector2(tileViewData.TileSideLength, tileViewData.TileSideLength);
            tileView.transform.AsRectTransform().anchoredPosition = new Vector2(tileViewData.xPosition, tileViewData.yPosition);
            tileView.Setup(tileViewData.TileColor, tileViewData.IsMainTile);
            return tileView;
        }

        public UnitMapView CreateUnitView(UnitMapView prefab, Sprite unitSprite, Vector2 position, float sideLength) {
            var unitView = Instantiate(prefab, this.transform);
            unitView.transform.AsRectTransform().sizeDelta = new Vector2(sideLength, sideLength);
            unitView.transform.AsRectTransform().anchoredPosition = new Vector2(position.x, position.y);
            unitView.SetViewData(unitSprite);
            return unitView;
        }

		public Vector2 GetMapPosition() {
            return transform.AsRectTransform().anchoredPosition;
		}
	}
}