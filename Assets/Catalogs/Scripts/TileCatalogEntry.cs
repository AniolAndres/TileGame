using UnityEngine;
using Assets.Views;
using System.Collections.Generic;

namespace Assets.Catalogs {

    [CreateAssetMenu(fileName = "Tiles Catalog", menuName = "ScriptableObjects/Catalogs/Create Tile Catalog Entry", order = 1)]
    public class TileCatalogEntry : CatalogEntry {

        [SerializeField]
        private TileView tilePrefab;

        [SerializeField]
        private TileType tileType;

        [SerializeField]
        private List<UnitCatalogEntry> spawnableUnits = new List<UnitCatalogEntry>();

        public TileView TilePrefab => tilePrefab;

        public TileType TileType => tileType;

        public List<UnitCatalogEntry> SpawnableUnits => spawnableUnits;

        public bool CanCreate => spawnableUnits.Count != 0;
    }
}