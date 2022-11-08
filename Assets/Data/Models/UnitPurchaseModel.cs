using Assets.Catalogs;
using System;

namespace Assets.Data {
    public class UnitPurchaseModel {

        private readonly UnitCatalogEntry unitCatalogEntry;

        private readonly bool hasEnoughFunds;

        public UnitPurchaseModel(UnitCatalogEntry unitCatalogEntry, bool hasEnoughFunds) {
            this.unitCatalogEntry = unitCatalogEntry;
            this.hasEnoughFunds = hasEnoughFunds;
        }

        public bool CanPay() {
            return hasEnoughFunds;
        }

        public string GetUnitId() {
            return unitCatalogEntry.Id;
        }
    }
}