
using Assets.Views;
using Assets.Data.Models;

namespace Assets.Controllers {
    public class TileMapController {

        private TileController[,] map;

        private TileMapView mapView;

        public TileMapController(int width, int height) {
            map = new TileController[width,height];
        }
    
        public void SetUp() {
            for(int i = 0; i < map.GetLength(0); ++i) {
                for(int j = 0; j < map.GetLength(1); ++j) {

                    var view = mapView.InstantiateTileView(i,j);
                    var model = new TileModel();

                    map[i, j] = new TileController(model,view);
                }
            }
        }

        public TileController GetTileAtPosition(int x, int y) {
            return map[x,y];
        }

    }
}