
using Assets.Catalogs;
using System;
using Assets.Catalogs.Scripts;
using Assets.Configs;
using Assets.Data.Levels;

namespace Assets.Data.Models {
    public class GameStateModel {

        private readonly LevelData currentLevel;

        private readonly UnitsCatalog unitsCatalog;

        private readonly TilesCatalog tilesCatalog;

        private readonly CommandersCatalog commandersCatalog;

        private readonly ArmyColorsCatalog armyColorsCatalog;
        
		public bool IsAttacking { get; set; }

		public GameStateModel(UnitsCatalog unitsCatalog, TilesCatalog tilesCatalog, 
            CommandersCatalog commandersCatalog, ArmyColorsCatalog armyColorsCatalog, string levelId, ILevelProvider levelProvider) {
            currentLevel = levelProvider.GetLevel(levelId);
            this.commandersCatalog = commandersCatalog;
            this.unitsCatalog = unitsCatalog;
            this.tilesCatalog = tilesCatalog;
            this.armyColorsCatalog = armyColorsCatalog;
        }

        public CommanderCatalogEntry GetCommanderEntry(string commanderId) {
            return commandersCatalog.GetEntry(commanderId);
        }

        public UnitCatalogEntry GetUnitCatalogEntry(string unitId)
        {
            return unitsCatalog.GetEntry(unitId);
        }

        public bool IsBuilding(string tileDataTypeId)
        {
            return tilesCatalog.GetEntry(tileDataTypeId).CanBeControlled;
        }

        public ArmyColorCatalogEntry GetArmyEntry(string colorId) {
            return armyColorsCatalog.GetEntry(colorId);
        }

		public TileCatalogEntry GetTileEntryById(string tileDataTypeId) {
			return tilesCatalog.GetEntry(tileDataTypeId);
		}

        public bool IsSpawnableBuilding(string typeId) {
            var tileEntry = tilesCatalog.GetEntry(typeId);
            if(tileEntry.CanCreate && !tileEntry.CanBeControlled) {
                throw new NotSupportedException($"Tile Entry {typeId} can create but can't be controlled, it should not be possible, check the catalog entry");
            }

            return tileEntry.CanCreate;
        }

        public MapData GetCurrentLevelMapData()
        {
            return currentLevel.MapData;
        }

        public PlayerData[] GetCurrentLevelPlayerData()
        {
            return currentLevel.Players;
        }
    }
}