
using Assets.Catalogs;
using System;
using Assets.Data.Models;
using Assets.Views;
using Assets.Data.Level;
using Assets.Catalogs.Scripts;

namespace Assets.Controllers {
    public class TileControllerFactory {

        public ITileController GetTileController(TileCatalogEntry entry, TileData data, TileView view) {

            switch (entry.TileType) {
                case TileType.Building:
                    var model = new BuildingTileModel(entry, data.Position);
                    return new BuildingTileController(view, model);
                case TileType.Grass:
                case TileType.Water:
                    var genericTileModel = new TileModel(entry, data.Position);
                    return new TerrainTileController(view, genericTileModel);
            }

            throw new NotSupportedException($"Couldn't find any controller for type {entry.TileType.ToString()}, check it's added to the factory");
        }
    }
}