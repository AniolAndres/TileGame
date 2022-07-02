using Assets.Views;
using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Configs;

namespace Assets.Catalogs.Scripts {

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