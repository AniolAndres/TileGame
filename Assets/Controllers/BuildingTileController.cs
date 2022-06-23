using UnityEngine;
using System.Collections;
using Assets.Data.Models;
using Assets.Views;
using System;

namespace Assets.Controllers {
    public class BuildingTileController :  BaseTileController<BuildingTileView, BuildingTileModel>, ITileController {
        public BuildingTileController(TileView view, TileModel model) : base(view,model) {
        }

        public void OnCreate() {
            view.OnTilePressed += PushPopupState;
        }

        private void PushPopupState() {
            
        }

        public void OnDestroy() {
            view.OnTilePressed -= PushPopupState;
        }
    }
}