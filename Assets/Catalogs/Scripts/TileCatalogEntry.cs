﻿using UnityEngine;
using Assets.Views;
using System.Collections.Generic;

namespace Assets.Catalogs {

    [CreateAssetMenu(fileName = "Tiles Catalog", menuName = "ScriptableObjects/Catalogs/Create Tile Catalog Entry", order = 1)]
    public class TileCatalogEntry : CatalogEntry {

        [SerializeField]
        private Color tileColor;

        [SerializeField]
        private List<UnitCatalogEntry> spawnableUnits = new List<UnitCatalogEntry>();

        [SerializeField]
        private bool canBeControlled;

        [SerializeField]
        private float fundsMultiplier;

        public List<UnitCatalogEntry> SpawnableUnits => spawnableUnits;

        public Color TileColor => tileColor;

        public bool CanBeControlled => canBeControlled;

        public float FundsMultiplier => fundsMultiplier;

        public bool CanCreate => spawnableUnits.Count != 0;
    }
}