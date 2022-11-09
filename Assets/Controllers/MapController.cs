﻿using UnityEngine;
using Assets.Views;
using Assets.Data.Models;
using System;
using Assets.Catalogs;
using Assets.Data.Level;
using Assets.Data;

namespace Assets.Controllers {
    public class MapController {

        private TileController[,] map;
        private TileMapView tileMapView;
        private TileMapModel model;

        public event Action<TileData> OnTileClicked;

        public MapController(TileMapView tileMapView, TileMapModel model) {
            this.tileMapView = tileMapView;
            this.model = model;
        }

        public void CreateMap() {

            var levelData = model.GetLevelData();
            map = new TileController[levelData.Width, levelData.Height];

            SetUp(levelData);
        }

        private void SetUp(LevelData levelData) {

            var time = DateTime.Now;

            var sideLength = model.GetSideLength();

            foreach (var tile in levelData.TileData) {
                CreateTile(tile, sideLength);
            }

            var timeAfterLoad = DateTime.Now;

            var loadingTime = timeAfterLoad - time;

            Debug.Log($"Created level, took {loadingTime.TotalMilliseconds} ms");
        }

        public void OnDestroy() {

            var mapWidth = map.GetLength(0);
            var mapHeight = map.GetLength(1);

            for (int i = 0; i < mapWidth; ++i) {
                for (int j = 0; j < mapHeight; ++j) {
                    map[i,j].OnDestroy();
                    map[i,j].OnTileClicked -= FireTileClickedEvent;
                }
            }
        }

        private void FireTileClickedEvent(TileData data) {
            OnTileClicked?.Invoke(data);
        }

        public Vector2 GetRealTilePosition(Vector2Int tilePosition) {
            return model.GetRealTileWorldPosition(tilePosition);
        }

        private void CreateTile(TileData tileData, float sideLength) {

            var tileEntry = model.GetTileEntry(tileData.TypeId);
            var tilePosition = model.GetRealTileWorldPosition(tileData.Position);
            var tileViewPrefab = tileEntry.TilePrefab;
            var tileView = tileMapView.InstantiateTileView(tileViewPrefab, tilePosition.x, tilePosition.y, sideLength);
            var tileModel = new TileModel(tileEntry, tileData.Position);
            var tileController = new TileController(tileView, tileModel);
            tileController.OnTileClicked += FireTileClickedEvent;
            tileController.OnCreate();
            map[tileData.Position.x, tileData.Position.y] = tileController;    
        }

        public UnitMapView CreateUnit(UnitCatalogEntry unitEntry, BuyUnitData buyUnitData) {
            var tilePosition = model.GetRealTileWorldPosition(buyUnitData.Position);
            var sideLength = model.GetSideLength();
            return tileMapView.CreateUnitView(unitEntry.UnitView, unitEntry.UnitPurchaseViewConfig.UnitSprite, tilePosition, sideLength);
        }

        public void HighlightAvailableTiles(Vector2Int tileDataPosition, string unitId)
        {
            var unitEntry = model.GetUnitCatalogEntry(unitId);
            
        }
    }
}