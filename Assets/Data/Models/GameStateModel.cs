using UnityEngine;
using System.Collections;
using Assets.Data.Levels;
using Assets.Data.Level;

namespace Assets.Data.Models {
    public class GameStateModel {

        private readonly ILevelProvider levelProvider;

        public GameStateModel(ILevelProvider levelProvider) {
            this.levelProvider = levelProvider;
        }

        public LevelData GetLevelData() {

            //Get current level or smth
            
            return levelProvider.GetLevel();
        }
    }
}