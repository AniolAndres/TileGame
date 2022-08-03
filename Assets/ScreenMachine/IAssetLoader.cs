using UnityEngine;
using System.Collections;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

namespace Assets.ScreenMachine {
    public interface IAssetLoader {

        Task LoadAsync();

        T GetPrefabAsset<T>(AssetReference reference);

        ScriptableObject GetScriptableObject(AssetReference reference);

        void AddPrefabReference(AssetReference reference);

        void AddScriptableObjectReference(AssetReference reference);

        void Dispose();
    }
}