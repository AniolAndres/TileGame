using Assets.Views;
using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Configs;
using UnityEngine.AddressableAssets;

namespace Assets.Catalogs.Scripts {

    [CreateAssetMenu(fileName = "States Catalog", menuName = "ScriptableObjects/Catalogs/Create States Catalog Entry", order = 1)]
    public class StateCatalogEntry : CatalogEntry{

        [SerializeField]
        private AssetReference uiView;

        [SerializeField]
        private AssetReference worldView;

        [SerializeField]
        private List<StateAsset> stateAssets;

        public AssetReference UiView => uiView;

        public AssetReference WorldView => worldView;

        public T GetStateAsset<T>() {
            foreach(var asset in stateAssets) {
                if(asset is T found) {
                    return found;
                }
            }

            throw new NotSupportedException($"Could not find any state asset of type {typeof(T).FullName}");
        }
 
    }

}