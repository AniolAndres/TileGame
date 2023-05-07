
using Assets.Catalogs;
using System;
using Assets.Data.Level;
using Assets.Catalogs.Scripts;
using Assets.Configs;
using Unity.Plastic.Newtonsoft.Json;

namespace Assets.Data.Models {
    public class GameStateModel {

        private readonly SerializableLevelData currentLevel;

        private readonly UnitsCatalog unitsCatalog;

        private readonly TilesCatalog tilesCatalog;

        private readonly CommandersCatalog commandersCatalog;

        private readonly ArmyColorsCatalog armyColorsCatalog;

		public bool IsAttacking { get; set; }

		public GameStateModel(LevelsCatalog levelsCatalog, UnitsCatalog unitsCatalog, TilesCatalog tilesCatalog, 
            CommandersCatalog commandersCatalog, ArmyColorsCatalog armyColorsCatalog, string levelId) {
            var levelJson = levelsCatalog.GetEntry(levelId).LevelJson.ToString();
            currentLevel = JsonConvert.DeserializeObject<SerializableLevelData>(levelJson);
            this.commandersCatalog = commandersCatalog;
            this.unitsCatalog = unitsCatalog;
            this.tilesCatalog = tilesCatalog;
            this.armyColorsCatalog = armyColorsCatalog;
        }

        public int GetTotalPlayers() {
            return currentLevel.playersCount;
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
            return tilesCatalog.GetEntry(tileDataTypeId).CanBeControlled;
        }

        public ArmyColorCatalogEntry GetArmyEntry(SetupArmyData armyData) {
            return armyColorsCatalog.GetEntry(armyData.ArmyColorId);
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
    }
}