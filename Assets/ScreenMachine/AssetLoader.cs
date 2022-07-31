using Assets.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.ScreenMachine {
    public class AssetLoader : IAssetLoader{

        private readonly List<AssetReference> assetsToLoad = new List<AssetReference>();

        private readonly Dictionary<AssetReference, AsyncOperationHandle<GameObject>> assetsLoaded = new Dictionary<AssetReference, AsyncOperationHandle<GameObject>>();

        public event Action<AssetLoader> OnDispose;

        public void AddReference(AssetReference reference) {
            if (assetsToLoad.Contains(reference)) {
                return;
            }
            assetsToLoad.Add(reference);
        }

        public T GetAsset<T>(AssetReference reference){
            var asset = assetsLoaded[reference].Result;
            return asset.GetComponent<T>();
        }

        public Task LoadAsync() {

            var taskList = new List<Task>(assetsToLoad.Count);

            foreach(var asset in assetsToLoad) {

                var handle = Addressables.LoadAssetAsync<GameObject>(asset);

                if (assetsLoaded.ContainsKey(asset)) {
                    throw new NotSupportedException($"Trying to load asset {asset} twice! Check asset loader");
                }

                taskList.Add(handle.Task);
                assetsLoaded[asset] = handle;          
            }

            assetsToLoad.Clear();

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