using System.Collections.Generic;
using UnityEngine;

namespace Assets.ScreenMachine {
    public class AssetLoaderFactory {

        private readonly List<AssetLoader> activeLoaders = new List<AssetLoader>();

        public IAssetLoader CreateLoader() {
            var loader = new AssetLoader();
            loader.OnDispose += Dispose;
            activeLoaders.Add(loader);
            Debug.Log($"Current loader count = {activeLoaders.Count}");
            return loader;
        }

        private void Dispose(AssetLoader loader) {
            loader.OnDispose -= Dispose;
            activeLoaders.Remove(loader);
        }
    }
}