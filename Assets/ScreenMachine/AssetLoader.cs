using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.ScreenMachine {
    public class AssetLoader : IAssetLoader{

        private readonly List<AssetReference> prefabAssetsToLoad = new List<AssetReference>();

        private readonly List<AssetReference> scriptablesAssetsToLoad = new List<AssetReference>();

        private readonly Dictionary<AssetReference, AsyncOperationHandle<GameObject>> prefabsAssetsLoaded = new Dictionary<AssetReference, AsyncOperationHandle<GameObject>>();

        private readonly Dictionary<AssetReference, AsyncOperationHandle<ScriptableObject>> scriptableAssetsLoaded = new Dictionary<AssetReference, AsyncOperationHandle<ScriptableObject>>();

        public event Action<AssetLoader> OnDispose;

        public void AddPrefabReference(AssetReference reference) {
            if (prefabAssetsToLoad.Contains(reference) || prefabsAssetsLoaded.ContainsKey(reference)) {
                return;
            }
            prefabAssetsToLoad.Add(reference);
        }

        public void AddScriptableObjectReference(AssetReference reference) {
            if (scriptablesAssetsToLoad.Contains(reference) || scriptableAssetsLoaded.ContainsKey(reference)) {
                return;
            }
            scriptablesAssetsToLoad.Add(reference);
        }

        public T GetPrefabAsset<T>(AssetReference reference){
            var asset = prefabsAssetsLoaded[reference].Result;
            return asset.GetComponent<T>();
        }
        public ScriptableObject GetScriptableObject(AssetReference reference) {
            return scriptableAssetsLoaded[reference].Result;
        }

        public Task LoadAsync() {

            var taskList = new List<Task>(prefabAssetsToLoad.Count + scriptablesAssetsToLoad.Count);

            LoadPrefabs(taskList);

            LoadScriptableObjects(taskList);

            return Task.WhenAll(taskList);
        }

        public void DisposeStateLoadedAssets(List<AssetReference> viewsAssetReferences, List<AssetReference> stateAssetsReferences) {
            prefabAssetsToLoad.Clear();
            scriptablesAssetsToLoad.Clear();

            foreach(var asset in viewsAssetReferences) {
                ReleasePrefabReference(asset);
            }

            foreach(var asset in stateAssetsReferences) {
                ReleaseScriptableReference(asset);
            }

            OnDispose?.Invoke(this);
        }

        private void ReleaseScriptableReference(AssetReference assetReference) {
            if (assetReference.RuntimeKeyIsValid()) {
                Addressables.Release(scriptableAssetsLoaded[assetReference]);
            }

            scriptableAssetsLoaded.Remove(assetReference);
        }

        private void ReleasePrefabReference(AssetReference assetReference) {
            if (assetReference.RuntimeKeyIsValid()) {
                Addressables.Release(prefabsAssetsLoaded[assetReference]);
            }

            prefabsAssetsLoaded.Remove(assetReference);
        }

        private void LoadScriptableObjects(List<Task> taskList) {
            foreach (var asset in scriptablesAssetsToLoad) {

                var handle = Addressables.LoadAssetAsync<ScriptableObject>(asset);

                if (scriptableAssetsLoaded.ContainsKey(asset)) {
                    throw new NotSupportedException($"Trying to load asset {asset} twice! Check asset loader");
                }

                taskList.Add(handle.Task);
                scriptableAssetsLoaded[asset] = handle;
            }

            scriptablesAssetsToLoad.Clear();
        }

        private void LoadPrefabs(List<Task> taskList) {
            foreach (var asset in prefabAssetsToLoad) {

                var handle = Addressables.LoadAssetAsync<GameObject>(asset);

                if (prefabsAssetsLoaded.ContainsKey(asset)) {
                    throw new NotSupportedException($"Trying to load asset {asset} twice! Check asset loader");
                }

                taskList.Add(handle.Task);
                prefabsAssetsLoaded[asset] = handle;
            }

            prefabAssetsToLoad.Clear();
        }
    }
}