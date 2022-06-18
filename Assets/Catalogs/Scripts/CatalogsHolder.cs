using UnityEngine;


namespace Assets.Catalogs.Scripts {

    [CreateAssetMenu(fileName = "Catalogs", menuName = "ScriptableObjects/Create Catalogs catalog", order = 1)]
    public class CatalogsHolder : ScriptableObject {

        [SerializeField]
        private StatesCatalog statesCatalog;

        [SerializeField]
        private LevelsCatalog levelsCatalog;
        
        [SerializeField]
        private TilesCatalog tilesCatalog;

        public StatesCatalog StatesCatalog => statesCatalog;

        public LevelsCatalog LevelsCatalog => levelsCatalog;

        public TilesCatalog TilesCatalog => tilesCatalog;
    }
}