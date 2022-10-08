using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.ScreenMachine;
using Assets.Data;

namespace Assets.Controllers {
    public abstract class BaseStateController<TuiView, TWorldView> 
        where TuiView : UiView 
        where TWorldView : WorldView {

        protected TuiView uiView;

        protected TWorldView worldView;

        protected Context context;

        protected IAssetLoaderFactory assetLoaderFactory => context.AssetLoaderFactory;

        protected ITimerFactory timerFactory => context.TimerFactory;

        private List<ScriptableObject> stateAssets = new List<ScriptableObject>();

        private IScreenMachine screenMachine => context.ScreenMachine;

        public BaseStateController(Context context) {
            this.context = context;
        }

        protected T GetStateAsset<T>() where T : ScriptableObject {
            foreach(var stateAsset in stateAssets) {
                if(stateAsset is T) {
                    return stateAsset as T;
                }
            }

            throw new NotSupportedException("Couldn't find any state asset of type " + typeof(T).FullName);
        }

        protected void PopState() {
            screenMachine.PopState();
        }

        protected void PushState(IStateBase state) {
            screenMachine.PushState(state);
        }

        protected void PresentState(IStateBase state) {
            screenMachine.PresentState(state);
        }

        public void OnUpdate() {
            uiView.OnUpdate();
            worldView.OnUpdate();
        }

        public void DisableRaycasts() {
            uiView.DisableRaycast();
            worldView.DisableRaycast();
        }

        public void EnableRaycasts() {
            uiView.EnableRaycast();
            worldView.EnableRaycast();
        }

        public void CacheStateAssets(List<ScriptableObject> stateAssets) {
            this.stateAssets = stateAssets;
        }

        public void LinkViews(UiView uiView, WorldView worldView) {
            this.uiView = uiView as TuiView;
            this.worldView = worldView as TWorldView;
        }

        public void DestroyViews() {
            GameObject.Destroy(uiView.gameObject);
            GameObject.Destroy(worldView.gameObject);
        }

        public void ReleaseAssets(string stateId) {
            context.AssetLoaderFactory.ReleaseStateLoadedAssets(stateId);
        }

    }
}