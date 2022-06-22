using UnityEngine;
using System.Collections;
using Assets.Data.Models;
using Assets.Views;

namespace Assets.Controllers {
    public class BuildingTileController :  BaseTileController<BuildingTileView, BuildingTileModel>, ITileController {
        public BuildingTileController(TileView view, TileModel model) : base(view,model) {
        }

        public void OnCreate() {
            
        }

        public void OnDestroy() {
            
        }
    }
}