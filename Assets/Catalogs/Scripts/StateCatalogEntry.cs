using Assets.Views;
using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Configs;
using Assets.ScreenMachine;
using UnityEngine.AddressableAssets;

namespace Assets.Catalogs {

    [CreateAssetMenu(fileName = "States Catalog", menuName = "ScriptableObjects/Catalogs/Create States Catalog Entry", order = 1)]
    public class StateCatalogEntry : CatalogEntry{

        [SerializeField]
        private UiView uiView;

        [SerializeField]
        private WorldView worldView;

        [SerializeField]
        private List<StateAsset> stateAssets;

        public UiView UiView => uiView;

        public WorldView WorldView => worldView;

        public List<StateAsset> StateAssets => stateAssets;

        // public List<AssetReference> GetViewsAssetReferences() {
        //     var referenceList = new List<AssetReference>();
        //     referenceList.Add(uiView);
        //     referenceList.Add(worldView);
        //     return referenceList;
        // }
        //
        // public List<StateAsset> GetAllAssetReferences() {
        //     var referenceList = new List<AssetReference>();
        //     referenceList.Add(uiView);
        //     referenceList.Add(worldView);
        //     foreach(var asset in stateAssets) {
        //         referenceList.Add(asset);
        //     }
        //     return referenceList;
        // }
    }

}