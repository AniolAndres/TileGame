
using Assets.Catalogs.Scripts;

namespace Assets.Data.Models {
    public class GameStateModel {

        private readonly LevelCatalogEntry currentLevel;

        public GameStateModel(LevelsCatalog levelsCatalog, string levelId) {
            this.currentLevel = levelsCatalog.GetEntry(levelId);
        }

        public int GetTotalPlayers() {
            return currentLevel.PlayersCount;
        }
    }
}