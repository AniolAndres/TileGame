using UnityEngine;
using System.Collections;

namespace Assets.Catalogs.Scripts {

    [CreateAssetMenu(fileName = "LevelCatalogEntry", menuName = "ScriptableObjects/Catalogs/Create level Catalog Entry", order = 1)]
    public class LevelCatalogEntry : CatalogEntry {

        [SerializeField]
        private Vector2Int size;

        [SerializeField]
        private float tileSideLength;

        public Vector2Int Size => size;

        public float TileSideLength => tileSideLength;

    }
}