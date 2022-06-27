
using System;
using System.Collections.Generic;
using Assets.Catalogs.Scripts;

namespace Assets.Data.Models {
    public class PopupStateModel {

        private readonly UnitsCatalog unitsCatalog;

        public PopupStateModel(UnitsCatalog unitsCatalog) {
            this.unitsCatalog = unitsCatalog;
        }

        public List<UnitCatalogEntry> GetUnits() {
            return unitsCatalog.GetAllEntries();
        }
    }
}