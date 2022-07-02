using UnityEngine;
using System.Collections;
using Assets.Configs;
using Assets.Views;

namespace Assets.Catalogs {

    [CreateAssetMenu(fileName = "PopupStateConfig", menuName = "ScriptableObjects/Configs/Create popupState config", order = 1)]
    public class PopupStateConfig : StateAsset {

        [SerializeField]
        private UnitPurchaseView unitPurchaseView;

        public UnitPurchaseView UnitPurchaseView => unitPurchaseView;

    }
    
}