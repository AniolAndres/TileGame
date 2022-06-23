using UnityEngine;
using Assets.Views;
using Assets.Data.Models;
using System;
using Assets.Data.Levels;
using Assets.Data.Level;

namespace Assets.Controllers {
    public class MapController {

        private ITileController[,] map;
        private TileMapView tileMapView;
        private TileMapModel model;

        public event Action OnTileClicked;

        public MapController(TileMapView tileMapView, TileMapModel model) {
            this.tileMapView = tileMapView;
            this.model = model;
        }

        public void CreateMap() {

            var levelData = model.GetLevelData();
            map = new ITileController[levelData.Width, levelData.Height];

            SetUp(levelData);
        }

        //private async void SetUpAsync() {

        //    var taskList = new List<Task>();

        //    var time = DateTime.Now;

        //    for (int i = 0; i < map.GetLength(0); ++i) {
        //        for (int j = 0; j < map.GetLength(1); ++j) {

        //            taskList.Add(Task.Run(() => CreateTile(i, j)));
                
        //        }
        //    }

        //    await Task.WhenAll(taskList);

        //    var timeAfterLoad = DateTime.Now;

        //    var timeloading = timeAfterLoad - time;

        //    Debug.Log($"Created level async, took {timeloading.TotalMilliseconds} ms");
        //}

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
                }
            }
        }

        private void CreateTile(TileData tileData, float sideLength) {

            var tileEntry = model.GetTileEntry(tileData.TypeId);
            var tilePosition = model.GetTilePosition(tileData.Position.x, tileData.Position.y);
            var tileViewPrefab = tileEntry.TilePrefab;
            var tileView = tileMapView.InstantiateTileView(tileViewPrefab, tilePosition.x, tilePosition.y, sideLength);
            var tileModel = new TileModel(tileEntry, tileData.Position);
            var tileController = new TerrainTileController(tileView, tileModel);
            tileController.OnCreate();
            map[tileData.Position.x, tileData.Position.y] = tileController;
            
        }

        public ITileController GetTileAtPosition(int x, int y) {
            return map[x, y];
        }

        private void FireTileClickedAction() {
            OnTileClicked?.Invoke();
        }
    }
}