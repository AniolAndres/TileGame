using UnityEngine;
using Assets.Views;
using Assets.Data.Models;
using System;
using Assets.Catalogs;
using Assets.Data.Level;
using Assets.Data;
using Assets.Extensions;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Assets.Controllers {
    public class MapController {

        private TileController[,] map;
        private BuildingHandler buildingHandler;
        private TileMapView view;
        private TileMapModel model;
        private readonly UnitHandler unitHandler;

        private MapPathFinder pathFinder;

        public event Action<TileData> OnTileClicked;

        public event Action OnMapClicked;

        public MapController(TileMapView tileMapView, TileMapModel model, UnitHandler unitHandler, BuildingHandler buildingHandler) {
            this.view = tileMapView;
            this.model = model;
            this.unitHandler = unitHandler;
            this.buildingHandler = buildingHandler;
        }

        public void OnCreate() {

            var levelData = model.GetLevelData();
            map = new TileController[levelData.Width, levelData.Height];

            SetUp(levelData);

            pathFinder = new MapPathFinder(map, unitHandler, model.MovementTypesCatalog);
            pathFinder.Init();
        }

        private void SetUp(LevelData levelData) {

            var time = DateTime.Now;

            var sideLength = model.GetSideLength();

            foreach (var tile in levelData.TileData) {
                CreateTile(tile);
            }

            var timeAfterLoad = DateTime.Now;

            var loadingTime = timeAfterLoad - time;

            Debug.Log($"Created level, took {loadingTime.TotalMilliseconds} ms");

            //-----------------------------------

            void CreateTile(TileData tileData) {

                var tileEntry = model.GetTileEntry(tileData.TypeId);
                var tilePosition = model.GetRealTileWorldPosition(tileData.Position);
                var tileViewColor = tileEntry.TileColor;
                var tileView = view.InstantiateTileView(tileViewColor, tilePosition.x, tilePosition.y, sideLength);
                var tileModel = new TileModel(tileEntry, tileData.Position);
                var tileController = new TileController(tileView, tileModel);
                tileController.OnTileClicked += FireTileClickedEvent;
                tileController.OnCreate();
                map[tileData.Position.x, tileData.Position.y] = tileController;

                var isBuilding = model.IsBuilding(tileEntry);
                if (isBuilding) {
                    var player = model.GetRandomArmyInfo(); 
                    buildingHandler.AddBuilding(player.playerIndex, tileData.Position, tileController);
                    var color = model.GetColor(player.armyColorId);
                    tileView.SetOwnerColor(color); // need colors from players
                }              
            }
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

        public UnitMapView CreateUnit(UnitCatalogEntry unitEntry, BuyUnitData buyUnitData) {
            var tilePosition = model.GetRealTileWorldPosition(buyUnitData.Position);
            var sideLength = model.GetSideLength();
            return view.CreateUnitView(unitEntry.UnitView, unitEntry.UnitPurchaseViewConfig.UnitSprite, tilePosition, sideLength);
        }

        public List<Vector2Int> GetPath(Vector2Int destination) {
            return pathFinder.GetPath(destination);
        }

        public void HighlightAvailableTiles(Vector2Int tileDataPosition, int currentArmyId, string unitId)
        {
            var unitEntry = model.GetUnitCatalogEntry(unitId);
            var availableTiles = pathFinder.GetAvailableTiles(unitEntry, currentArmyId, tileDataPosition);
            
            foreach(var tile in availableTiles) {
                var tileController = map.GetElement(tile.position);
                var orientation = GetArrowOrientation(tile);
                tileController.HighLight(tile.accumulatedCost, orientation);
            }

            //------------------------------------

            float GetArrowOrientation(Node tile) {

                if(tile.previousNode == null) {
                    return 0;
                }

                var firstPosition = tile.position;
                var secondPosition = tile.previousNode.position;

                var vector = (secondPosition - firstPosition).ToVector2();
                var angle = Vector2.SignedAngle(Vector2.up, vector);             
                return angle;
            }
        }

        public List<Vector2> GetListOfRealPositions(List<Vector2Int> path) {
            var realPositions = new List<Vector2>(path.Count);

            foreach(var point in path) {
                realPositions.Add(GetRealTilePosition(point));
            }

            return realPositions;
        }

        public void ClearCurrentPathfinding() {
            var highlightedTiles = pathFinder.GetHighLightedTiles();
            foreach(var tile in highlightedTiles) {
                var tileController = map.GetElement(tile.position);
                tileController.RemoveHighlight();
            }
        }

        public float GetTileSize() {
            return model.GetSideLength();
        }

        public Vector2Int GetMapSize() {
            return model.GetSize();
        }

        public Vector2 GetMapCenterCurrentPosition() {
            return view.GetMapPosition();
        }
    }
}