using UnityEngine;
using System.Collections;
using System;
using Assets.Catalogs.Scripts;
using System.Linq;

namespace Assets.Data.Models {
    public class TileMapModel {

        private readonly LevelCatalogEntry currentLevelEntry;

        private const float tileSizeLength = 64f;

        public float TileSizeLength => tileSizeLength;


        public TileMapModel(LevelsCatalog levelsCatalog) {           
            this.currentLevelEntry = levelsCatalog.GetAllEntries().First();
        }

        public Vector2Int GetSize() {
            return currentLevelEntry.Size;
        }

        public Vector2 GetTilePosition(int x, int y) {
            return new Vector2((x + 0.5f) * tileSizeLength, (y + 0.5f) * TileSizeLength);
        }
    }
}