using System.Collections.Generic;
using Assets.Catalogs;
using Assets.Catalogs.Scripts;
using Assets.Views;

namespace Assets.Data.Models {
    public class PopupStateModel {

        private readonly UnitsCatalog unitsCatalog;

        private readonly PopupStateConfig popupStateConfig;

        public PopupStateModel(UnitsCatalog unitsCatalog, PopupStateConfig popupStateConfig) {
            this.unitsCatalog = unitsCatalog;
            this.popupStateConfig = popupStateConfig;
        }

        public List<UnitCatalogEntry> GetUnits() {
            return unitsCatalog.GetAllEntries();
        }

        public UnitPurchaseView GetUnitPrefab() {
            return popupStateConfig.UnitPurchaseView;
        }

        public UnitCatalogEntry GetUnitEntry(string unitId) {
            return unitsCatalog.GetEntry(unitId);
        }
    }
}