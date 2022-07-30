using UnityEngine;
using System.Collections;
using Assets.Configs;
using Assets.Views;

namespace Assets.Catalogs {

    [CreateAssetMenu(fileName = "CreateUnitStateConfig", menuName = "ScriptableObjects/Configs/Create CreateUnit config", order = 1)]
    public class CreateUnitStateConfig : StateAsset {

        [SerializeField]
        private UnitPurchaseView unitPurchaseView;

        public UnitPurchaseView UnitPurchaseView => unitPurchaseView;

    }
    
}