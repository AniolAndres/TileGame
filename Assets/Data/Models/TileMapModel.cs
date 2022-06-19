using UnityEngine;
using Assets.Catalogs.Scripts;
using System.Linq;
using Assets.Views;

namespace Assets.Data.Models {
    public class TileMapModel {

        private readonly LevelCatalogEntry currentLevelEntry;

        private readonly TilesCatalog tilesCatalog;

        private const float tileSizeLength = 64f;

        public float TileSizeLength => tileSizeLength;


        public TileMapModel(LevelsCatalog levelsCatalog, TilesCatalog tilesCatalog) {           
            this.currentLevelEntry = levelsCatalog.GetAllEntries().First();
            this.tilesCatalog = tilesCatalog;
        }

        public Vector2Int GetSize() {
            return currentLevelEntry.Size;
        }

        public Vector2 GetTilePosition(int x, int y) {
            return new Vector2((x - currentLevelEntry.Size.x/2f + 0.5f) * tileSizeLength, (y - currentLevelEntry.Size.y/2f + 0.5f) * TileSizeLength);
        }

        public TileView GetTilePrefab() {
            var randomTypeId = Random.Range(0f, 1f) > 0.5f ? "water" :"grass";
            var prefab = tilesCatalog.GetEntry(randomTypeId).TilePrefab;
            return prefab;
        }
    }
}