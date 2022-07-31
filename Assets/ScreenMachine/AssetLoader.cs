using Assets.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.ScreenMachine {
    public class AssetLoader : IAssetLoader{

        private readonly List<AssetReference> assetsToLoad = new List<AssetReference>();

        private readonly Dictionary<AssetReference, AsyncOperationHandle> assetsLoaded = new Dictionary<AssetReference, AsyncOperationHandle>();

        public event Action<AssetLoader> OnDispose;

        public void AddReference(AssetReference reference) {
            if (assetsToLoad.Contains(reference)) {
                return;
            }
            assetsToLoad.Add(reference);
        }

        public T GetAsset<T>(AssetReference reference){
            var asset = assetsLoaded[reference].Result;
            return (T)asset;
        }

        public Task LoadAsync() {

            var taskList = new List<Task>(assetsToLoad.Count);

            foreach(var asset in assetsToLoad) {

                var handle = Addressables.LoadAssetAsync<object>(asset);

                if (assetsLoaded.ContainsKey(asset)) {
                    throw new NotSupportedException($"Trying to load asset {asset} twice! Check asset loader");
                }

                taskList.Add(handle.Task);
                assetsLoaded[asset] = handle;          
            }

            return Task.WhenAll(taskList);
        }

        public void Dispose() {
            assetsToLoad.Clear();
            foreach(var asset in assetsLoaded) {
                Addressables.Release(asset.Value);
            }
            assetsLoaded.Clear();

            OnDispose?.Invoke(this);
        }
    }
}