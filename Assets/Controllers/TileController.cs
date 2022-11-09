using System;
using Assets.Data.Level;
using Assets.Data.Models;
using Assets.Views;

namespace Assets.Controllers {
    public class TileController{
        public event Action<TileData> OnTileClicked; 
        
        private readonly TileView view;

        private readonly TileModel model;

        public TileController(TileView view, TileModel model) {
            this.view = view;
            this.model = model;
        }
        
        public void OnCreate() {
            view.OnTilePressed += FireTerrainClickedEvent;
        }

        private void FireTerrainClickedEvent() {
            var tileData = model.GetTileData();
            OnTileClicked?.Invoke(tileData);
        }

        public void OnDestroy() {
            view.OnTilePressed -= FireTerrainClickedEvent;
        }
    }
}