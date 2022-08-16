

using Assets.Catalogs;
using Assets.Views;
using System;

namespace Assets.Data {
    public class PlayerModel {

        private readonly CommanderCatalogEntry commanderEntry;

        public PlayerModel(CommanderCatalogEntry commanderCatalogEntry) {
            this.commanderEntry = commanderCatalogEntry;
        }

        public CommanderViewData GetCommanderViewData() {
            return new CommanderViewData {
                Color = commanderEntry.Color,
                FullName = commanderEntry.FullName
            };
        }
    }
}