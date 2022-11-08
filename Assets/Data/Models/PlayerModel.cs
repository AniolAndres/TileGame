

using Assets.Catalogs;
using Assets.Views;
using System;

namespace Assets.Data {
    public class PlayerModel {

        private readonly CommanderCatalogEntry commanderEntry;

        private readonly string armyId;

        public PlayerModel(CommanderCatalogEntry commanderCatalogEntry, string armyId) {
            this.commanderEntry = commanderCatalogEntry;
            this.armyId = armyId;
        }

        public CommanderViewData GetCommanderViewData() {
            return new CommanderViewData {
                Color = commanderEntry.Color,
                FullName = commanderEntry.FullName
            };
        }

        public string GetArmyId()
        {
            return armyId;
        }
    }
}