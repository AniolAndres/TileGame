
using Assets.Catalogs;
using Assets.Data.Level;
using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Assets.Data.Levels {

    public class LevelProvider : ILevelProvider {

        private readonly LevelsCatalog levelsCatalog;

        private readonly TilesCatalog tilesCatalog;

        public LevelProvider(LevelsCatalog levelsCatalog, TilesCatalog tilesCatalog) {
            this.levelsCatalog = levelsCatalog;
            this.tilesCatalog = tilesCatalog;
        }

        public LevelData GetLevel(string levelId)
        {
            var allTileEntries = tilesCatalog.GetAllEntries();
            var tileDataList = new List<TileData>();

            var entry = levelsCatalog.GetEntry(levelId);

            for (int i = 0; i < entry.Size.x; ++i) {
                for (int j = 0; j < entry.Size.y; ++j) {
                    tileDataList.Add(new TileData { TypeId = GetRandomType(),
                    Position = new UnityEngine.Vector2Int(i,j)});
                }
            }


            if (tileDataList.IsNullOrEmpty()) {
                throw new NotSupportedException("Tile list was not build properly, check Level Provider");
            }

            return new LevelData { 
                PlayersCount = entry.PlayersCount,
                TileData = tileDataList,
                Width = entry.Size.x,
                Height = entry.Size.y};


            string GetRandomType()
            {
                return allTileEntries[Random.Range(0, allTileEntries.Count)].Id;
            }
        }
    }

}