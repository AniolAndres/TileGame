


using Assets.Data.Models;
using Assets.Views;

namespace Assets.Controllers {
    public class TileController {

        private TileModel model;

        private TileView view;

        public TileController(TileModel model, TileView view) {
            this.model = model;
            this.view = view;
        }

    }
}