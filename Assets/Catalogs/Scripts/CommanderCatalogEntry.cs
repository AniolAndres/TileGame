using UnityEngine;

namespace Assets.Catalogs {
    [CreateAssetMenu(fileName = "CommanderCatalogEntry", menuName = "ScriptableObjects/Catalogs/Create Commander Catalog Entry", order = 1)]
    public class CommanderCatalogEntry : CatalogEntry {

        [SerializeField]
        private string fullName;

        public string FullName => fullName;
    }
}