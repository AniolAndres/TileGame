
using Assets.Catalogs;
using System;
using Assets.Data.Level;
using Assets.Catalogs.Scripts;

namespace Assets.Data.Models {
    public class GameStateModel {

        private readonly LevelCatalogEntry currentLevel;

        private readonly UnitsCatalog unitsCatalog;

        private readonly TilesCatalog tilesCatalog;

        private readonly CommandersCatalog commandersCatalog;

        private readonly ArmyColorsCatalog armyColorsCatalog;

		public bool IsAttacking { get; set; }

		public GameStateModel(LevelsCatalog levelsCatalog, UnitsCatalog unitsCatalog, TilesCatalog tilesCatalog, 
            CommandersCatalog commandersCatalog, ArmyColorsCatalog armyColorsCatalog, string levelId) {
            this.currentLevel = levelsCatalog.GetEntry(levelId);
            this.commandersCatalog = commandersCatalog;
            this.unitsCatalog = unitsCatalog;
            this.tilesCatalog = tilesCatalog;
            this.armyColorsCatalog = armyColorsCatalog;
        }

        public int GetTotalPlayers() {
            return currentLevel.PlayersCount;
        }

        public CommanderCatalogEntry GetCommanderEntry(SetupArmyData armyData) {
            return commandersCatalog.GetEntry(armyData.CommanderId);
        }

        public UnitCatalogEntry GetUnitCatalogEntry(string unitId)
        {
            return unitsCatalog.GetEntry(unitId);
        }

        public bool IsBuilding(string tileDataTypeId)
        {
            return tilesCatalog.GetEntry(tileDataTypeId).CanCreate;
        }

        public ArmyColorCatalogEntry GetArmyEntry(SetupArmyData armyData) {
            return armyColorsCatalog.GetEntry(armyData.ArmyColorId);
        }

		public TileCatalogEntry GetTileEntryById(string tileDataTypeId) {
			return tilesCatalog.GetEntry(tileDataTypeId);
		}
	}
}