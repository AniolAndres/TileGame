using System.Collections.Generic;
using Assets.Catalogs;
using Assets.Catalogs.Scripts;
using Assets.Views;

namespace Assets.Data.Models {
    public class PopupStateModel {

        private readonly TilesCatalog tilesCatalog;

        private readonly UnitsCatalog unitsCatalog;

        private readonly PopupStateConfig popupStateConfig;

        public PopupStateModel(UnitsCatalog unitsCatalog, TilesCatalog tilesCatalog, PopupStateConfig popupStateConfig) {
            this.unitsCatalog = unitsCatalog;
            this.tilesCatalog = tilesCatalog;
            this.popupStateConfig = popupStateConfig;
        }

        public List<UnitCatalogEntry> GetUnits(string tileId) {
            var entry = tilesCatalog.GetEntry(tileId);
            return entry.SpawnableUnits;
        }

        public UnitPurchaseView GetUnitPrefab() {
            return popupStateConfig.UnitPurchaseView;
        }

        public UnitCatalogEntry GetUnitEntry(string unitId) {
            return unitsCatalog.GetEntry(unitId);
        }
    }
}