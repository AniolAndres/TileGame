
using Assets.Catalogs;
using System;

namespace Assets.Data.Models {
    public class GameStateModel {

        private readonly LevelCatalogEntry currentLevel;

        private readonly CommandersCatalog commandersCatalog;

        public GameStateModel(LevelsCatalog levelsCatalog, CommandersCatalog commandersCatalog, string levelId) {
            this.currentLevel = levelsCatalog.GetEntry(levelId);
            this.commandersCatalog = commandersCatalog;
        }

        public int GetTotalPlayers() {
            return currentLevel.PlayersCount;
        }

        public CommanderCatalogEntry GetCommanderEntry(string commanderId) {
            return commandersCatalog.GetEntry(commanderId);
        }

    }
}