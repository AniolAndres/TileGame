using UnityEngine;
using System.Collections;
using Assets.Catalogs;
using System;

namespace Assets.Data {
    public class UnitModel {

        private readonly UnitCatalogEntry unitCatalogEntry;

        private readonly int armyIndex;

        private bool canMove;

        public bool CanMove => canMove;

        public UnitModel(UnitCatalogEntry unitCatalogEntry, int armyIndex) {
            this.unitCatalogEntry = unitCatalogEntry;
            this.armyIndex = armyIndex;
        }

        public void RefreshUnit()
        {
            canMove = true;
        }

        public string GetId() {
            return unitCatalogEntry.Id;
        }

        public int GetArmyIndex() {
            return armyIndex;
        }

        public void SetToAlreadyMoved()
        {
            canMove = false;
        }
    }
}