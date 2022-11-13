using System.Collections.Generic;
using UnityEngine;

namespace Assets.Catalogs
{
    [CreateAssetMenu(fileName = "MovementTypeCatalogEntry", menuName = "ScriptableObjects/Catalogs/Create Movement Type Catalog Entry", order = 1)]
    public class MovementTypeCatalogEntry : CatalogEntry
    {
        [SerializeField] 
        private Sprite typeIcon;

        [SerializeField] 
        private List<MovementCostPair> movementCostPair;

        public Sprite TypeIcon => typeIcon;

        public List<MovementCostPair> MovementCostPairList => movementCostPair;
    }
}