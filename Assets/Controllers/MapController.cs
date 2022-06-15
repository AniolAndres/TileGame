using UnityEngine;
using System.Collections;
using Assets.Views;
using Assets.Data.Models;

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

        private void SetUp() {
            for (int i = 0; i < map.GetLength(0); ++i) {
                for (int j = 0; j < map.GetLength(1); ++j) {

                    var view = tileMapView.InstantiateTileView(i, j);
                    var tileModel = new TileModel();

                    map[i, j] = new TileController(tileModel, view);
                }
            }
        }

        public TileController GetTileAtPosition(int x, int y) {
            return map[x, y];
        }
    }
}