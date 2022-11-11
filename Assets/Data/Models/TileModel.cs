using UnityEngine;
using Assets.Catalogs;
using Assets.Data.Level;

namespace Assets.Data.Models {
    public class TileModel {

        private readonly TileCatalogEntry tileEntry;

        protected Vector2Int Position { get; }

        protected string TypeId => tileEntry.Id;

        public TileModel(TileCatalogEntry tileEntry, Vector2Int position) {
            this.Position = position;
            this.tileEntry = tileEntry;
        }

        public TileData GetTileData() {
            return new TileData {
                Position = Position,
                TypeId = TypeId
            };
        }

        public string GetTileType()
        {
            return TypeId;
        }
    }
}