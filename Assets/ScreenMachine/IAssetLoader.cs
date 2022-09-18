using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assets.ScreenMachine {
    public interface IAssetLoader {

        Task LoadAsync();

        T GetAsset<T>(AssetReference reference) where T : UnityEngine.Object;

        void AddReference(AssetReference reference);

    }
}