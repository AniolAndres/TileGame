using Assets.Data.Models;
using Assets.Views;
using System;
using UnityEngine;

namespace Assets.Controllers {
    public class TileController {

        private TileModel model;

        private TileView view;

        public event Action OnTileClicked;


        public TileController(TileModel model, TileView view) {
            this.model = model;
            this.view = view;
        }

        public void OnCreate() {
            view.OnTilePressed += ReactToTilePressed;
        }

        public void OnDestroy() {
            view.OnTilePressed -= ReactToTilePressed;
        }

        private void ReactToTilePressed() {
            var position = model.Position;
            Debug.Log($"Tile {position.x},{position.y} pressed");

            OnTileClicked?.Invoke();
        }
    }
}