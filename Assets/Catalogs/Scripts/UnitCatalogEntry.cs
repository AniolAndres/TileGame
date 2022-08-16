using UnityEngine;
using System.Collections;
using Assets.Views;
using Assets.Configs;

namespace Assets.Catalogs {

    [CreateAssetMenu(fileName = "UnitCatalogEntry", menuName = "ScriptableObjects/Catalogs/Create Unit Catalog Entry", order = 1)]
    public class UnitCatalogEntry : CatalogEntry {

        [SerializeField]
        private UnitMapView unitView;

        [SerializeField]
        private UnitPurchaseViewConfig unitPurchaseViewConfig;

        [SerializeField]
        private UnitSpecificationConfig unitSpecificationConfig;

        public UnitMapView UnitView => unitView;

        public UnitPurchaseViewConfig UnitPurchaseViewConfig => unitPurchaseViewConfig;

        public UnitSpecificationConfig UnitSpecificationConfig => unitSpecificationConfig;
    }
}