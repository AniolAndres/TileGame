using UnityEngine;
using System.Collections;

namespace Assets.Controllers {
    public class MapController {

        private TileMap map;

        public void CreateMap(int width, int height) {
            map = new TileMap(width, height);
        }
        
    }
}