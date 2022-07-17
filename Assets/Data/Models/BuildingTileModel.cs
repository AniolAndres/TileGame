using UnityEngine;
using System.Collections;
using Assets.Catalogs.Scripts;
using System;
using Assets.Data.Level;

namespace Assets.Data.Models {
    public class BuildingTileModel : TileModel {
        public BuildingTileModel(TileCatalogEntry tileCatalogEntry, Vector2Int position) : base(tileCatalogEntry, position) {
        }

    }
}