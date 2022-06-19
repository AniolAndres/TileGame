using UnityEngine;
using System.Collections;
using Assets.Views;
using Assets.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Assets.Controllers {
    public class MapController {


        private TileController[,] map;
        private TileMapView tileMapView;
        private TileMapModel model;


        public MapController(TileMapView tileMapView, TileMapModel model) {
            this.tileMapView = tileMapView;
            this.model = model;
        }

        public void CreateMap() {
            var size = model.GetSize();
            map = new TileController[size.x, size.y];

            SetUp();
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

        private void SetUp() {

            var time = DateTime.Now;

            var mapWidth = map.GetLength(0);
            var mapHeight = map.GetLength(1);

            tileMapView.CenterCamera(mapWidth * model.TileSizeLength, mapHeight * model.TileSizeLength);

            for (int i = 0; i < mapWidth; ++i) {
                for (int j = 0; j < mapHeight; ++j) {

                    CreateTile(i, j);

                }
            }

            var timeAfterLoad = DateTime.Now;

            var timeloading = timeAfterLoad - time;

            Debug.Log($"Created level, took {timeloading.TotalMilliseconds} ms");
        }

        private void CreateTile(int x, int y) {

            var tilePosition = model.GetTilePosition(x,y);

            var view = tileMapView.InstantiateTileView(tilePosition.x, tilePosition.y);
            var tileModel = new TileModel();
            map[x, y] = new TileController(tileModel, view);
        }

        public TileController GetTileAtPosition(int x, int y) {
            return map[x, y];
        }
    }
}