using UnityEngine;
using System.Collections;
using Assets.Catalogs.Scripts;
using System;

namespace Assets.Data {
    public class UnitModel {

        private readonly UnitCatalogEntry unitCatalogEntry;

        public UnitModel(UnitCatalogEntry unitCatalogEntry) {
            this.unitCatalogEntry = unitCatalogEntry;
        }

        public string GetId() {
            return unitCatalogEntry.Id;
        }

    }
}