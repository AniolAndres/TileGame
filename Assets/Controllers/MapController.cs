using UnityEngine;
using System.Collections;

namespace Assets.Controllers {
    public class MapController {

        private TileMapController map;

        public void CreateMap(int width, int height) {
            map = new TileMapController(width, height);
        }
        
    }
}