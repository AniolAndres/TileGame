using UnityEngine;
using Assets.Catalogs.Scripts;
using System.Linq;
using Assets.Views;
using Assets.Data.Levels;
using Assets.Data.Level;
using System;

namespace Assets.Data.Models {
    public class TileMapModel {

        private readonly LevelCatalogEntry currentLevelEntry;

        private readonly TilesCatalog tilesCatalog;

        private readonly UnitsCatalog unitsCatalog;

        private readonly ILevelProvider levelProvider;

        private readonly string levelId;

        public TileMapModel(LevelsCatalog levelsCatalog, TilesCatalog tilesCatalog, UnitsCatalog unitsCatalog,ILevelProvider levelProvider, string levelId) {           
            this.currentLevelEntry = levelsCatalog.GetAllEntries().First();
            this.tilesCatalog = tilesCatalog;
            this.unitsCatalog = unitsCatalog;
            this.levelProvider = levelProvider;
            this.levelId = levelId;
        }

        public Vector2Int GetSize() {
            return currentLevelEntry.Size;
        }

        public LevelData GetLevelData() {
            return levelProvider.GetLevel(levelId);
        }

        public Vector2 GetRealTileWorldPosition(Vector2Int tilePosition) {
            return new Vector2((tilePosition.x - currentLevelEntry.Size.x/2f + 0.5f) * currentLevelEntry.TileSideLength, 
                (tilePosition.y - currentLevelEntry.Size.y/2f + 0.5f) * currentLevelEntry.TileSideLength);
        }

        public float GetSideLength() {
            return currentLevelEntry.TileSideLength;
        }

        public TileCatalogEntry GetTileEntry(string typeId) {
            return tilesCatalog.GetEntry(typeId);
        }

        public UnitCatalogEntry GetUnitCatalogEntry(string unitId) {
            return unitsCatalog.GetEntry(unitId);
        }

        public bool IsBuilding(string tileType) {
            var tileEntry = tilesCatalog.GetEntry(tileType);
            return tileEntry.CanCreate;
        }
    }
}