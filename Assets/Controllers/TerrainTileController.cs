
using Assets.Views;
using Assets.Data.Models;
using System;
using Assets.Data.Level;

namespace Assets.Controllers {
    public class TerrainTileController : BaseTileController<TileView, TileModel>, ITileController {

        public event Action<TileData> OnTileClicked; 

        public TerrainTileController(TileView view, TileModel model) : base(view, model) {
        }

        public void OnCreate() {
            
        }

        public void OnDestroy() {
            
        }
    }
}