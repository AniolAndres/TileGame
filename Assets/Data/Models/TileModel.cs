using UnityEngine;
using Assets.Catalogs;
using Assets.Data.Level;
using System;

namespace Assets.Data.Models {
    public class TileModel {

        private readonly TileCatalogEntry tileEntry;

        protected Vector2Int Position { get; }

        protected string TypeId => tileEntry.Id;

        public TileModel(TileCatalogEntry tileEntry, Vector2Int position) {
            this.Position = position;
            this.tileEntry = tileEntry;
        }

        public Tile GetTileData() {
            return new Tile {
                Position = Position,
                TypeId = TypeId
            };
        }

        public string GetTileType()
        {
            return TypeId;
        }

        public int GetFunds() {
            var fundsForBuilding = 1000;
            var floatingFunds = tileEntry.FundsMultiplier * fundsForBuilding;
            return Mathf.CeilToInt(floatingFunds);
        }
    }
}