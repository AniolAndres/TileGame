using System;
using Assets.Data;
using Assets.Data.Level;
using Assets.Data.Models;
using Assets.Views;
using Assets.Views.ViewData;

namespace Assets.Controllers {
    public class TileController{
        
        private readonly TileView view;

        private readonly TileModel model;

        public TileController(TileView view, TileModel model) {
            this.view = view;
            this.model = model;
        }

        public string GetTileType() {
            return model.GetTileType();
        }

        public void HighLight(int cost, float orientation) {
            view.Highlight(cost, orientation);
        }

        public void RemoveHighlight() {
            view.RemoveHighlight();
        }

        public int GetFunds() {
            return model.GetFunds();
        }

		public void SetState(TileState state) {
            view.SetState(state);
		}
	}
}