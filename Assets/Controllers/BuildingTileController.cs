using UnityEngine;
using System.Collections;
using Assets.Data.Models;
using Assets.Views;
using System;
using Assets.Data.Level;

namespace Assets.Controllers {
    public class BuildingTileController :  BaseTileController<BuildingTileView, BuildingTileModel>, ITileController {

        public event Action<TileData> OnTileClicked;

        public BuildingTileController(TileView view, TileModel model) : base(view,model) {
        }

        public void OnCreate() {
            view.OnTilePressed += PushPopupState;
        }

        private void PushPopupState() {
            var tileData = model.GetTileData();
            OnTileClicked?.Invoke(tileData);
        }

        public void OnDestroy() {
            view.OnTilePressed -= PushPopupState;
        }
    }
}