using UnityEngine;
using Assets.Views;
using Assets.Data.Models;
using System;
using Assets.Data.Level;
using Assets.Data;

namespace Assets.Controllers {
    public class MapController {

        private ITileController[,] map; //Maybe a dictionary would be better?
        private TileMapView tileMapView;
        private TileMapModel model;
        private readonly UnitHandler unitHandler;

        private readonly TileControllerFactory tileControllerFactory = new TileControllerFactory();

        public event Action<TileData> OnBuildingClicked;

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
                    map[i,j].OnTileClicked -= FireTileClickedEvent;
                }
            }
        }

        private void FireTileClickedEvent(TileData data) {
            if (unitHandler.HasUnitSelected) {
                MoveSelectedUnit(data);
                return;
            }


            if (!unitHandler.IsSpaceEmpty(data.Position)) {
                unitHandler.SetUnitSelected(data.Position);
                return;
            }

            var isBuilding = model.IsBuilding(data.TypeId);
            if (!isBuilding) {
                return;
            }

            OnBuildingClicked?.Invoke(data);
        }

        private void MoveSelectedUnit(TileData data) {
            var realNewPosition = model.GetRealTileWorldPosition(data.Position);
            unitHandler.MoveSelectedUnit(data.Position, realNewPosition);
        }

        private void CreateTile(TileData tileData, float sideLength) {

            var tileEntry = model.GetTileEntry(tileData.TypeId);
            var tilePosition = model.GetRealTileWorldPosition(tileData.Position);
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
            var tilePosition = model.GetRealTileWorldPosition(buyUnitData.Position);
            var sideLength = model.GetSideLength();
            var unitView = tileMapView.CreateUnitView(unitEntry.UnitView, unitEntry.UnitPurchaseViewConfig.UnitSprite, tilePosition, sideLength);
            unitView.OnMovementEnd += unitHandler.TryUnlockInput;
            var unitController = new UnitController(unitView, unitModel);
            unitHandler.AddUnit(unitController, buyUnitData.Position);
        }

        public void RemoveUnit(Vector2Int position) {
            var removedController = unitHandler.GetUnitControllerAtPosition(position);
            removedController.OnDestroy();
            unitHandler.RemoveUnitAtPosition(position);
        }

    }
}