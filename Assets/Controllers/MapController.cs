using UnityEngine;
using Assets.Views;
using Assets.Data.Models;
using System;
using Assets.Catalogs;
using Assets.Data.Level;
using Assets.Data;
using Assets.Extensions;
using System.Collections.Generic;
using Assets.Configs;
using Assets.Views.ViewData;
using UnityEngine.UIElements;

namespace Assets.Controllers {
    public class MapController {

        private TileController[,] map;
        private BuildingHandler buildingHandler;
        private TileMapView view;
        private TileMapModel model;
        private readonly UnitHandler unitHandler;
        private TileCursorController cursorController;

        private MapPathFinder pathFinder;

        public event Action OnMapClicked;

        public MapController(TileMapView tileMapView, TileMapModel model, UnitHandler unitHandler, 
            BuildingHandler buildingHandler) {
            this.view = tileMapView;
            this.model = model;
            this.unitHandler = unitHandler;
            this.buildingHandler = buildingHandler;
        }

        public void OnCreate() {

            view.OnMapClicked += FireMapClickedEvent;

            var levelData = model.GetMapData();
            map = new TileController[levelData.Width, levelData.Height];

            SetUp(levelData);

            pathFinder = new MapPathFinder(map, unitHandler, model.MovementTypesCatalog);
            pathFinder.Init();

            cursorController = new TileCursorController(view.TileCursorView);
        }

        private void FireMapClickedEvent() {
            OnMapClicked?.Invoke();
        }

        private void SetUp(MapData levelData) {

            var time = DateTime.Now;

            var sideLength = model.GetSideLength();

            foreach (var tile in levelData.Tiles) {
                CreateTile(tile);
            }

            var timeAfterLoad = DateTime.Now;

            var loadingTime = timeAfterLoad - time;

            Debug.Log($"Created level, took {loadingTime.TotalMilliseconds} ms");

            //-----------------------------------

            void CreateTile(TileData tileData) {

				var tileEntry = model.GetTileEntry(tileData.tileType);
				var position = new Vector2Int(tileData.xPosition, tileData.yPosition);

				var tileViewData = GetTileViewData();
                var tileView = view.InstantiateTileView(ref tileViewData);
                var tileModel = new TileModel(tileEntry, position);
                var tileController = new TileController(tileView, tileModel);

                map[position.x, position.y] = tileController;

                var isBuilding = model.IsBuilding(tileEntry);
                if (isBuilding) {
                    var player = model.GetPlayerData(tileData.owner); 
                    buildingHandler.AddBuilding(player.PlayerIndex, position, tileController);
                    var color = model.GetColor(player.ColorId);
                    tileView.SetOwnerColor(color); // need colors from players
                }

                //--------

				TileViewData GetTileViewData() {

					var tilePosition = model.GetRealTileWorldPosition(position);

					return new TileViewData {
                        TileColor = tileEntry.TileColor,
                        xPosition = tilePosition.x,
                        yPosition = tilePosition.y,
                        IsMainTile = false,
                        TileSideLength = sideLength
                    };
				}
			}
        }

        public void OnDestroy() {
			view.OnMapClicked -= FireMapClickedEvent;
        }

        public Vector2 GetRealTilePosition(Vector2Int tilePosition) {
            return model.GetRealTileWorldPosition(tilePosition);
        }

        public UnitMapView CreateUnitView(UnitCatalogEntry unitEntry, BuyUnitData buyUnitData) {
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

        public string GetTypeFromTile(Vector2Int position) {
            var tile = map[position.x,position.y];
            return tile.GetTileType();
        }

		public List<Vector2Int> GetTilesInRange(Vector2Int origin, UnitSpecificationConfig unitSpecConfig) {
			var tilesInRange = new List<Vector2Int>();

            var minRange = unitSpecConfig.MinRange;
            var maxRange = unitSpecConfig.MaxRange;

            var maxX = map.GetLength(0) - 1;
            var maxY = map.GetLength(1) - 1;

            var originX = origin.x;
            var originY = origin.y;

            for (int x = -maxRange; x <= maxRange; x++) {
                var currentX = originX + x;

                if(currentX < 0 || currentX > maxX) {
                    continue; //Out of bounds on X axis
                }

                for (int y =  -maxRange; y <= maxRange; y++) {
                    var currentY = originY + y;
					if (currentY < 0 || currentY > maxY) {
						continue; //Out of bounds on Y axis
					}

                    var absoluteDistance = Mathf.Abs(x) + Mathf.Abs(y);

                    if (absoluteDistance < minRange || absoluteDistance > maxRange) {
                        continue; //Out of range, either min or max
                    }

                    tilesInRange.Add(new Vector2Int(currentX, currentY));

				}
            }
            
            
            return tilesInRange;
		}

        public void HighlightTilesInRange(List<Vector2Int> tilesInRange) {

            foreach (var tile in tilesInRange) {
                var controller = map[tile.x, tile.y];
                controller.SetState(TileState.InRangeForAttack);
            }
        }

		public void MoveTileCursorTo(Vector2Int tile) {
            var realPosition = GetRealTilePosition(tile);
            cursorController.SetPosition(realPosition);
        }

        public void ClearAttackableTiles(List<Vector2Int> tilesInRange) {
			foreach (var tile in tilesInRange) {
				var controller = map[tile.x, tile.y];
				controller.SetState(TileState.None);
			}
		}

		public bool IsOutOfBounds(Vector2Int tilePosition) {
			var maxX = map.GetLength(0);
			var maxY = map.GetLength(1);

            var currentX = tilePosition.x;
            var currentY = tilePosition.y;

            if(currentX < 0 || currentX >= maxX) {
                return true;
            }

			if (currentY < 0 || currentY >= maxY) {
				return true;
			}

            return false;
		}
    }
}