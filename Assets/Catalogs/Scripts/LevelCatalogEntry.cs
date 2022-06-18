using UnityEngine;
using System.Collections;

namespace Assets.Catalogs.Scripts {

    [CreateAssetMenu(fileName = "LevelCatalogEntry", menuName = "ScriptableObjects/Create level Catalog Entry", order = 1)]
    public class LevelCatalogEntry : CatalogEntry {

        [SerializeField]
        private Vector2Int size;

        public Vector2Int Size => size;

    }
}