

using Assets.Catalogs.Scripts;
using System;

namespace Assets.Data {
    public class UnitPurchaseModel {

        private readonly UnitCatalogEntry unitCatalogEntry;

        public UnitPurchaseModel(UnitCatalogEntry unitCatalogEntry) {
            this.unitCatalogEntry = unitCatalogEntry;
        }

        public bool CanPay() {
            return true;
        }

        public string GetUnitId() {
            return unitCatalogEntry.Id;
        }
    }
}