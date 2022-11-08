using UnityEngine;
using System.Collections;
using Assets.Catalogs;
using System;

namespace Assets.Data {
    public class UnitModel {

        private readonly UnitCatalogEntry unitCatalogEntry;

        private readonly string armyId;

        public UnitModel(UnitCatalogEntry unitCatalogEntry, string armyId) {
            this.unitCatalogEntry = unitCatalogEntry;
            this.armyId = armyId;
        }

        public string GetId() {
            return unitCatalogEntry.Id;
        }

        public string GetArmyId() {
            return armyId;
        }

    }
}