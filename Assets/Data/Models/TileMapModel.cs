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

        public TileMapModel(LevelsCatalog levelsCatalog, TilesCatalog tilesCatalog, UnitsCatalog unitsCatalog,ILevelProvider levelProvider) {           
            this.currentLevelEntry = levelsCatalog.GetAllEntries().First();
            this.tilesCatalog = tilesCatalog;
            this.unitsCatalog = unitsCatalog;
            this.levelProvider = levelProvider;
        }

        public Vector2Int GetSize() {
            return currentLevelEntry.Size;
        }

        public LevelData GetLevelData() {
            return levelProvider.GetLevel(); ;
        }

        public Vector2 GetRealTileWorldPosition(int x, int y) {
            return new Vector2((x - currentLevelEntry.Size.x/2f + 0.5f) * currentLevelEntry.TileSideLength, 
                (y - currentLevelEntry.Size.y/2f + 0.5f) * currentLevelEntry.TileSideLength);
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
    }
}