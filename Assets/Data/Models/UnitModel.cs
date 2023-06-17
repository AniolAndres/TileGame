using UnityEngine;
using System.Collections;
using Assets.Catalogs;
using System;

namespace Assets.Data {
    public class UnitModel {

        private readonly UnitCatalogEntry unitCatalogEntry;

        private readonly int armyIndex;

        private bool canMove;

        private int currentHp = 1000;

        public bool CanMove => canMove;

        public UnitModel(UnitCatalogEntry unitCatalogEntry, int armyIndex) {
            this.unitCatalogEntry = unitCatalogEntry;
            this.armyIndex = armyIndex;
        }

        public void SetHp(int newHp) {
            currentHp = newHp;
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

		public int GetNormalizedHp() {
			var hpFloat = (float)currentHp;
			return Mathf.CeilToInt(hpFloat / 100);
		}

		public void SetToAlreadyMoved()
        {
            canMove = false;
        }

        public int GetAbsoluteHp() {
            return currentHp;
        }
    }
}