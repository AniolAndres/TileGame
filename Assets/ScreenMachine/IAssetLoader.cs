using UnityEngine;
using System.Collections;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

namespace Assets.ScreenMachine {
    public interface IAssetLoader {

        Task LoadAsync();

        T GetAsset<T>(AssetReference reference);

        void AddReference(AssetReference reference);
        void Dispose();
    }
}