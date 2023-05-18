
using Assets.Catalogs.Scripts;
using UnityEngine;


namespace Assets.Catalogs {

    [CreateAssetMenu(fileName = "Catalogs", menuName = "ScriptableObjects/Catalogs/Create Catalogs catalog", order = 1)]
    public class CatalogsHolder : ScriptableObject {

        [SerializeField]
        private StatesCatalog statesCatalog;

        [SerializeField]
        private LevelsCatalog levelsCatalog;
        
        [SerializeField]
        private TilesCatalog tilesCatalog;

        [SerializeField]
        private UnitsCatalog unitsCatalog;

        [SerializeField]
        private CommandersCatalog commandersCatalog;

        [SerializeField] 
        private MovementTypesCatalog movementTypesCatalog;

        [SerializeField]
        private ArmyColorsCatalog armyColorsCatalog;

        [SerializeField]
        private DialogsCatalog dialogsCatalog;

        public StatesCatalog StatesCatalog => statesCatalog;

        public LevelsCatalog LevelsCatalog => levelsCatalog;

        public TilesCatalog TilesCatalog => tilesCatalog;

        public UnitsCatalog UnitsCatalog => unitsCatalog;

        public CommandersCatalog CommandersCatalog => commandersCatalog;

        public MovementTypesCatalog MovementTypesCatalog => movementTypesCatalog;

        public ArmyColorsCatalog ArmyColorsCatalog => armyColorsCatalog;

        public DialogsCatalog DialogsCatalog => dialogsCatalog;
    }
}