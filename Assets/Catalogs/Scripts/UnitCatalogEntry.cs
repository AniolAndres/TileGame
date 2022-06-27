using UnityEngine;
using System.Collections;
using Assets.Views;

namespace Assets.Catalogs.Scripts {


    [CreateAssetMenu(fileName = "UnitCatalogEntry", menuName = "ScriptableObjects/Catalogs/Create Unit Catalog Entry", order = 1)]
    public class UnitCatalogEntry : CatalogEntry {

        [SerializeField]
        private UnitMapView unitView;

        [SerializeField]
        private UnitPurchaseView unitPurchaseView;

        public UnitMapView UnitView => unitView;

        public UnitPurchaseView UnitPurchaseView => unitPurchaseView;
    }
}