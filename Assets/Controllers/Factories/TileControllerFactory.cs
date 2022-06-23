
using Assets.Catalogs;
using System;
using Assets.Data.Models;
using Assets.Views;

namespace Assets.Controllers {
    public class TileControllerFactory {

        public ITileController GetTileController(TileType type, TileView view, TileModel model) {

            switch (type) {
                case TileType.Building:
                    return new BuildingTileController(view, model);
                case TileType.Grass:
                    return new TerrainTileController(view, model);
                case TileType.Water:
                    return new TerrainTileController(view, model);
            }

            throw new NotSupportedException($"Couldn't find any controller for type {type.ToString()}, check it's added to the factory");
        }
    }
}