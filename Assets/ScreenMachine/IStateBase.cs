using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.ScreenMachine {
    public interface IStateBase {
        string GetId();

        void LinkViews(UiView uiView, WorldView worldView);

        void DestroyViews();

        void OnBringToFront();

        void OnSendToBack();

        void OnCreate();

        void OnDestroy();

        void OnUpdate();
        void EnableRaycasts();
        void DisableRaycasts();

        void CacheStateAssets(List<ScriptableObject> stateAssets);
        void ReleaseAssets(string stateId);
    }

}