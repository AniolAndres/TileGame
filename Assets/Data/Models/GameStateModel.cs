
using Assets.Catalogs;
using System;
using Assets.Data.Level;

namespace Assets.Data.Models {
    public class GameStateModel {

        private readonly LevelCatalogEntry currentLevel;

        private readonly UnitsCatalog unitsCatalog;

        private readonly TilesCatalog tilesCatalog;

        private readonly CommandersCatalog commandersCatalog;

        public GameStateModel(LevelsCatalog levelsCatalog, UnitsCatalog unitsCatalog, TilesCatalog tilesCatalog, CommandersCatalog commandersCatalog, string levelId) {
            this.currentLevel = levelsCatalog.GetEntry(levelId);
            this.commandersCatalog = commandersCatalog;
            this.unitsCatalog = unitsCatalog;
            this.tilesCatalog = tilesCatalog;
        }

        public int GetTotalPlayers() {
            return currentLevel.PlayersCount;
        }

        public CommanderCatalogEntry GetCommanderEntry(string commanderId) {
            return commandersCatalog.GetEntry(commanderId);
        }

        public bool DoesBuildingBelongToPlayer(TileData tileData)
        {
            return true;
        }

        public UnitCatalogEntry GetUnitCatalogEntry(string unitId)
        {
            return unitsCatalog.GetEntry(unitId);
        }

        public bool IsBuilding(string tileDataTypeId)
        {
            return tilesCatalog.GetEntry(tileDataTypeId).CanCreate;
        }
    }
}