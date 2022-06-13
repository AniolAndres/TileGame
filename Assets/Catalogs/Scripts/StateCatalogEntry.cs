using UnityEngine;

namespace Assets.Catalogs.Scripts {

    [CreateAssetMenu(fileName = "States Catalog", menuName = "ScriptableObjects/Create States Catalog Entry", order = 1)]
    public class StateCatalogEntry : CatalogEntry {

        [SerializeField]
        private UiView uiView;

        [SerializeField]
        private WorldView worldView;

        public UiView UiView => uiView;

        public WorldView WorldView => worldView;
    }

}