using System.Collections.Generic;
using Assets.Catalogs;
using Assets.Catalogs;
using Assets.Views;

namespace Assets.Data.Models {
    public class CreateUnitStateModel {

        private readonly TilesCatalog tilesCatalog;

        private readonly UnitsCatalog unitsCatalog;

        private readonly CreateUnitStateConfig popupStateConfig;

        public CreateUnitStateModel(UnitsCatalog unitsCatalog, TilesCatalog tilesCatalog, CreateUnitStateConfig popupStateConfig) {
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