using UnityEngine;
using System.Collections;
using Assets.Data.Models;
using Assets.Views;

namespace Assets.Controllers {
    public abstract class BaseTileController<TView, TModel>
        where TView : TileView
        where TModel : TileModel {

        protected TView view;

        protected TModel model;

        public BaseTileController(TileView view, TileModel model) {
            this.view = view as TView;
            this.model = model as TModel;
        }
    }
}