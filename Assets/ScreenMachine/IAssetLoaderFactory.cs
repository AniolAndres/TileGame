using UnityEngine;
using System.Collections;

namespace Assets.ScreenMachine {
    public interface IAssetLoaderFactory{

        IAssetLoader CreateLoader(IStateBase state);

        IAssetLoader CreateLoader(string stateId);
    }
}