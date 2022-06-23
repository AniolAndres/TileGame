using UnityEngine;
using System.Collections;
using Assets.Catalogs.Scripts;

namespace Assets.Data.Models {
    public class BuildingTileModel : TileModel {
        public BuildingTileModel(TileCatalogEntry tileCatalogEntry, Vector2Int position) : base(tileCatalogEntry, position) {
        }
    }
}