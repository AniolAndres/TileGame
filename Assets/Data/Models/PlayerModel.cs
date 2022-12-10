

using Assets.Catalogs;
using Assets.Catalogs.Scripts;
using Assets.Views;
using System;

namespace Assets.Data {
    public class PlayerModel {

        private readonly CommanderCatalogEntry commanderEntry;

        private readonly ArmyColorCatalogEntry armyColorEntry;

        public PlayerModel(CommanderCatalogEntry commanderCatalogEntry, ArmyColorCatalogEntry armyColorEntry) {
            this.commanderEntry = commanderCatalogEntry;
            this.armyColorEntry = armyColorEntry;
        }

        public CommanderViewData GetCommanderViewData() {
            return new CommanderViewData {
                Color = armyColorEntry.ArmyColor,
                FullName = commanderEntry.FullName
            };
        }

        public int GetArmyId()
        {
            return armyColorEntry.ArmyId;
        }
    }
}