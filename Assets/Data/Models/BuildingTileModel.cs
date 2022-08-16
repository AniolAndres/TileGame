using UnityEngine;
using Assets.Catalogs;

namespace Assets.Data.Models {
    public class BuildingTileModel : TileModel {
        public BuildingTileModel(TileCatalogEntry tileCatalogEntry, Vector2Int position) : base(tileCatalogEntry, position) {
        }

    }
}