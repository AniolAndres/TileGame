using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assets.ScreenMachine {
    public interface IAssetLoader {

        Task LoadAsync();

        T GetPrefabAsset<T>(AssetReference reference);

        ScriptableObject GetScriptableObject(AssetReference reference);

        void AddPrefabReference(AssetReference reference);

        void AddScriptableObjectReference(AssetReference reference);

        void DisposeStateLoadedAssets(List<AssetReference> viewsReferences, List<AssetReference> stateAssetsReferences);


    }
}