using System.Collections;
using UnityEngine;

namespace Assets.Catalogs.Scripts {

    [CreateAssetMenu(fileName = "ArmyCatalogEntry", menuName = "ScriptableObjects/Catalogs/Create Army Color Catalog Entry", order = 1)]
    public class ArmyColorCatalogEntry : CatalogEntry {

        [SerializeField]
        private Color armyColor;

        [SerializeField]
        private int armyId;

        public Color ArmyColor => armyColor;

        public int ArmyId => armyId;
    }
}