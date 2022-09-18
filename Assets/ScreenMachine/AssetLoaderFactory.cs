using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ScreenMachine {
    public class AssetLoaderFactory : IAssetLoaderFactory {

        private Dictionary<string, List<AssetLoader>> stateAssetLoaders = new Dictionary<string, List<AssetLoader>>();

        public IAssetLoader CreateLoader(IStateBase originState) {
            var id = originState.GetId();
            return CreateLoader(id);
        }

        public IAssetLoader CreateLoader(string id) {
            var loader = new AssetLoader();

            if (!stateAssetLoaders.ContainsKey(id)) {
                stateAssetLoaders[id] = new List<AssetLoader>();
            }
            stateAssetLoaders[id].Add(loader);

            return loader;
        }

        public void ReleaseStateLoadedAssets(string stateId) {
            if (!stateAssetLoaders.ContainsKey(stateId)) {
                return;
            }

            var loaderList = stateAssetLoaders[stateId];

            foreach(var loader in loaderList) {
                loader.Destroy();
            }

            stateAssetLoaders.Remove(stateId);
        }
    }
}