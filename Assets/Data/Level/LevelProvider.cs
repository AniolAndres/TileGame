
using Assets.Catalogs;
using Assets.Configs;
using Assets.Data.Level;
using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json;
using Random = UnityEngine.Random;

namespace Assets.Data.Levels {

    public class LevelProvider : ILevelProvider {

        private readonly LevelsCatalog levelsCatalog;

        public LevelProvider(LevelsCatalog levelsCatalog) {
            this.levelsCatalog = levelsCatalog;
        }

        public SerializableLevelData GetLevel(string levelId)
        {
            var entry = levelsCatalog.GetEntry(levelId);
            var levelDataString = entry.LevelJson.ToString();
            var levelData = JsonConvert.DeserializeObject<SerializableLevelData>(levelDataString);

            return levelData;
        }
    }

}