using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Configs;
using Assets.ScreenMachine;
using Assets.Data;
using Modules.AssetLoader;
using Modules.TimerFactory;

namespace Assets.Controllers {
    public abstract class BaseStateController<TuiView, TWorldView> 
        where TuiView : UiView 
        where TWorldView : WorldView {

        protected TuiView uiView;

        protected TWorldView worldView;

        protected Context context;

        protected IAssetLoaderFactory assetLoaderFactory => context.AssetLoaderFactory;

        protected ITimerFactory timerFactory => context.TimerFactory;

        private List<StateAsset> stateAssets = new List<StateAsset>();

        private IScreenMachine screenMachine => context.ScreenMachine;

        public event Action OnStateDestroyed;

        public event Action<BaseStateController<TuiView, TWorldView>> OnPostPopState;

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
            OnPostPopState?.Invoke(this);
        }

        protected void PushState(IStateBase state) {
            screenMachine.PushState(state);
        }

        protected void PresentState(IStateBase state) {
            screenMachine.PresentState(state);
        }

        public void OnUpdate() {
            uiView?.OnUpdate();
            worldView?.OnUpdate();
        }

        public void DisableRaycasts() {
            uiView?.DisableRaycast();
            worldView?.DisableRaycast();
        }

        public void EnableRaycasts() {
            uiView?.EnableRaycast();
            worldView?.EnableRaycast();
        }

        public void CacheStateAssets(List<StateAsset> assets) {
            this.stateAssets = assets;
        }

        public void LinkViews(UiView ui, WorldView world) {
            this.uiView = ui as TuiView;
            this.worldView = world as TWorldView;
        }

        public void DestroyViews() {
            if(uiView != null) {
                GameObject.Destroy(uiView.gameObject);
            }

            if(worldView != null) {
                GameObject.Destroy(worldView.gameObject);
            }
        }

        public void ReleaseAssets(string stateId) {
            context.AssetLoaderFactory.ReleaseStateLoadedAssets(stateId);
        }

    }
}