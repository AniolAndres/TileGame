

using Assets.Catalogs;
using Assets.Catalogs.Scripts;
using Assets.Views;
using System;

namespace Assets.Data {
    public class PlayerModel {

        private readonly CommanderCatalogEntry commanderEntry;

        private readonly ArmyColorCatalogEntry armyColorEntry;

        private readonly int PlayerIndex;
        
        private readonly int TeamId;
        
        public PlayerModel(CommanderCatalogEntry commanderCatalogEntry, ArmyColorCatalogEntry armyColorEntry,
            int playerIndex, int teamIndex) {
            this.commanderEntry = commanderCatalogEntry;
            this.armyColorEntry = armyColorEntry;
            this.PlayerIndex = playerIndex;
            this.TeamId = teamIndex;
        }

        public CommanderViewData GetCommanderViewData() {
            return new CommanderViewData {
                Color = armyColorEntry.ArmyColor,
                FullName = commanderEntry.FullName
            };
        }

        public int GetArmyIndex()
        {
            return PlayerIndex;
        }

        public string GetColorId() {
            return armyColorEntry.Id;
        }

        public string GetArmyCommanderId() {
            return commanderEntry.Id;
        }
    }
}