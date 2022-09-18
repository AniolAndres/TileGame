using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using Object = UnityEngine.Object;

namespace Assets.ScreenMachine {
    public class AssetLoader : IAssetLoader{

        private readonly List<AssetReference> assetsToLoad = new List<AssetReference>();

        private readonly Dictionary<AssetReference, AsyncOperationHandle<Object>> assetsLoaded = new Dictionary<AssetReference, AsyncOperationHandle<Object>>();

        public event Action<AssetLoader> OnDispose;

        public void AddReference(AssetReference reference) {
            if (assetsToLoad.Contains(reference) || assetsLoaded.ContainsKey(reference)) {
                return;
            }
            assetsToLoad.Add(reference);
        }

        public T GetAsset<T>(AssetReference reference) where T : Object {
            var asset = assetsLoaded[reference].Result;

            if(asset is ScriptableObject so) {
                return so as T;
            }

            if(asset is GameObject go) {
                return go.GetComponent<T>();
            }

            throw new NotSupportedException($"Couldn't find type of {typeof(T).FullName}");
        }

        public Task LoadAsync() {

            var taskList = new List<Task>(assetsToLoad.Count);

            LoadAssets(taskList);

            return Task.WhenAll(taskList);
        }

        public void DisposeStateLoadedAssets(List<AssetReference> viewsAssetReferences) {
            assetsToLoad.Clear();

            foreach(var asset in viewsAssetReferences) {
                ReleasePrefabReference(asset);
            }

            OnDispose?.Invoke(this);
        }


        private void ReleasePrefabReference(AssetReference assetReference) {
            if (assetsLoaded[assetReference].IsValid()) {
                Addressables.Release(assetsLoaded[assetReference]);
            }

            assetsLoaded.Remove(assetReference);
        }

        private void LoadAssets(List<Task> taskList) {
            foreach (var asset in assetsToLoad) {

                var handle = Addressables.LoadAssetAsync<Object>(asset);

                if (assetsLoaded.ContainsKey(asset)) {
                    throw new NotSupportedException($"Trying to load asset {asset} twice! Check asset loader");
                }

                taskList.Add(handle.Task);
                assetsLoaded[asset] = handle;
            }

            assetsToLoad.Clear();
        }
    }
}