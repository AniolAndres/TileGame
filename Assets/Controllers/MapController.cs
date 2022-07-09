using UnityEngine;
using Assets.Views;
using Assets.Data.Models;
using System;
using Assets.Data.Level;
using System.Collections.Generic;
using System.Linq;
using Assets.Data;

namespace Assets.Controllers {
    public class MapController {

        private ITileController[,] map;
        private TileMapView tileMapView;
        private TileMapModel model;
        private readonly UnitHandler unitHandler;

        private readonly TileControllerFactory tileControllerFactory = new TileControllerFactory();

        public event Action<UnitController> OnUnitClicked;

        public event Action<TileData> OnTerrainClicked;

        public MapController(TileMapView tileMapView, TileMapModel model, UnitHandler unitHandler) {
            this.tileMapView = tileMapView;
            this.model = model;
            this.unitHandler = unitHandler;
        }

        public void CreateMap() {

            var levelData = model.GetLevelData();
            map = new ITileController[levelData.Width, levelData.Height];

            SetUp(levelData);
        }

        private void SetUp(LevelData levelData) {

            var time = DateTime.Now;

            var sideLength = model.GetSideLength();

            foreach (var tile in levelData.TileData) {
                CreateTile(tile, sideLength);
            }

            var timeAfterLoad = DateTime.Now;

            var timeloading = timeAfterLoad - time;

            Debug.Log($"Created level, took {timeloading.TotalMilliseconds} ms");
        }

        public void OnDestroy() {

            var mapWidth = map.GetLength(0);
            var mapHeight = map.GetLength(1);

            for (int i = 0; i < mapWidth; ++i) {
                for (int j = 0; j < mapHeight; ++j) {
                    map[i,j].OnDestroy();
                    map[i, j].OnTileClicked -= FireTileClickedEvent;
                }
            }
        }

        private UnitController GetUnitControllerAtPosition(Vector2Int position) {
            return unitHandler.GetUnitControllerAtPosition(position);
        }

        private void FireTileClickedEvent(TileData data) {
            if (!unitHandler.IsSpaceEmpty(data.Position)) {
                var unitController = unitHandler.GetUnitControllerAtPosition(data.Position);
                OnUnitClicked?.Invoke(unitController);
                return;
            }

            OnTerrainClicked?.Invoke(data);
        }

        private void CreateTile(TileData tileData, float sideLength) {

            var tileEntry = model.GetTileEntry(tileData.TypeId);
            var tilePosition = model.GetRealTileWorldPosition(tileData.Position.x, tileData.Position.y);
            var tileViewPrefab = tileEntry.TilePrefab;
            var tileView = tileMapView.InstantiateTileView(tileViewPrefab, tilePosition.x, tilePosition.y, sideLength);
            var tileController = tileControllerFactory.GetTileController(tileEntry, tileData, tileView);
            tileController.OnTileClicked += FireTileClickedEvent;
            tileController.OnCreate();
            map[tileData.Position.x, tileData.Position.y] = tileController;    
        }

        public ITileController GetTileAtPosition(int x, int y) {
            return map[x, y];
        }

        public void CreateUnit(BuyUnitData buyUnitData) {
            var unitEntry = model.GetUnitCatalogEntry(buyUnitData.UnitId);
            var unitModel = new UnitModel(unitEntry);
            var tilePosition = model.GetRealTileWorldPosition(buyUnitData.Position.x, buyUnitData.Position.y);
            var sideLength = model.GetSideLength();
            var unitView = tileMapView.CreateUnitView(unitEntry.UnitView, tilePosition, sideLength);
            var unitController = new UnitController(unitView, unitModel);
            unitHandler.AddUnit(unitController, buyUnitData.Position);
        }
    }
}