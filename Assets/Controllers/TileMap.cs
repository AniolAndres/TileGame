using UnityEngine;
using System.Collections;

namespace Assets.Controllers {
    public class TileMap {

        private TileController[,] map;

        public TileMap(int width, int height) {
            map = new TileController[width,height];
        }

        public TileController GetTileAtPosition(int x, int y) {
            return map[x,y];
        }

    }
}