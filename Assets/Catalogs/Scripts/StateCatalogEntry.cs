using Assets.Views;
using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Configs;
using UnityEngine.AddressableAssets;

namespace Assets.Catalogs {

    [CreateAssetMenu(fileName = "States Catalog", menuName = "ScriptableObjects/Catalogs/Create States Catalog Entry", order = 1)]
    public class StateCatalogEntry : CatalogEntry{

        [SerializeField]
        private AssetReference uiView;

        [SerializeField]
        private AssetReference worldView;

        [SerializeField]
        private List<AssetReference> stateAssets;

        public AssetReference UiView => uiView;

        public AssetReference WorldView => worldView;

        public List<AssetReference> StateAssets => stateAssets;

        public List<AssetReference> GetViewsAssetReferences() {
            var referenceList = new List<AssetReference>();
            referenceList.Add(uiView);
            referenceList.Add(worldView);
            return referenceList;
        }

        public List<AssetReference> GetAllAssetReferences() {
            var referenceList = new List<AssetReference>();
            referenceList.Add(uiView);
            referenceList.Add(worldView);
            foreach(var asset in stateAssets) {
                referenceList.Add(asset);
            }
            return referenceList;
        }
    }

}