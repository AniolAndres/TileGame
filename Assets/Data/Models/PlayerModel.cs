

using Assets.Catalogs;
using Assets.Views;
using System;

namespace Assets.Data {
    public class PlayerModel {

        private readonly CommanderCatalogEntry commanderEntry;

        private readonly int armyIndex;

        public PlayerModel(CommanderCatalogEntry commanderCatalogEntry, int armyIndex) {
            this.commanderEntry = commanderCatalogEntry;
            this.armyIndex = armyIndex;
        }

        public CommanderViewData GetCommanderViewData() {
            return new CommanderViewData {
                Color = commanderEntry.Color,
                FullName = commanderEntry.FullName
            };
        }

        public int GetArmyId()
        {
            return armyIndex;
        }
    }
}