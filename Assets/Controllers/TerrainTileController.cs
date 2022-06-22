using UnityEngine;
using System.Collections;
using Assets.Views;
using Assets.Data.Models;

namespace Assets.Controllers {
    public class TerrainTileController : BaseTileController<TileView, TileModel>, ITileController {
        public TerrainTileController(TileView view, TileModel model) : base(view, model) {
        }

        public void OnCreate() {
            
        }

        public void OnDestroy() {
            
        }
    }
}