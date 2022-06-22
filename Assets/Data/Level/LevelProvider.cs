
using Assets.Catalogs.Scripts;
using Assets.Data.Level;
using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Data.Levels {

    public class LevelProvider : ILevelProvider {

        private readonly LevelsCatalog levelsCatalog;

        public LevelProvider(LevelsCatalog catalog) {
            this.levelsCatalog = catalog;
        }


        public LevelData GetLevel() {

            var tileDataList = new List<TileData>();

            var entry = levelsCatalog.GetAllEntries().First();

            for (int i = 0; i < entry.Size.x; ++i) {
                for (int j = 0; j < entry.Size.y; ++j) {
                    tileDataList.Add(new TileData { Type = TileType.Grass,
                    Position = new UnityEngine.Vector2Int(i,j)});
                }
            }


            if (tileDataList.IsNullOrEmpty()) {
                throw new NotSupportedException("Tile list was not build properly, check Level Provider");
            }

            return new LevelData { 
                TileData = tileDataList,
                Width = entry.Size.x,
                Height = entry.Size.y};
  
        }
    }

}