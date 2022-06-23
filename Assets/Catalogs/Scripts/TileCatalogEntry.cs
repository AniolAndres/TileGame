using UnityEngine;
using Assets.Views;

namespace Assets.Catalogs.Scripts {

    [CreateAssetMenu(fileName = "Tiles Catalog", menuName = "ScriptableObjects/Create Tile Catalog Entry", order = 1)]
    public class TileCatalogEntry : CatalogEntry {

        [SerializeField]
        private TileView tilePrefab;

        [SerializeField]
        private TileType tileType;

        public TileView TilePrefab => tilePrefab;

        public TileType TileType => tileType;
    }
}