using Assets.Catalogs;
using Assets.ScreenMachine;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controllers {

    public class GameScreenMachine : IScreenMachine {

        private Stack<IStateBase> screenStack;

        private StatesCatalog statesCatalog;

        private InputLocker inputLocker;

        private readonly AssetLoaderFactory assetLoaderFactory;

        private readonly TimerFactory timerFactory;

        private bool isLoading;

        const int CanvasOffset = 15;

        public GameScreenMachine(StatesCatalog statesCatalog, AssetLoaderFactory assetLoaderFactory, TimerFactory timerFactory) {
            this.statesCatalog = statesCatalog;
            this.assetLoaderFactory = assetLoaderFactory;
            this.timerFactory = timerFactory;
        }

        public void Init() {
            screenStack = new Stack<IStateBase>();
            inputLocker = new InputLocker();
        }

        public void PopState() {
            PopStateLocally();
        }

        public void PresentState(IStateBase state) {

            while (screenStack.Count != 0) {
                PopStateLocally();
            }

            PushStateLocally(state);
        }

        public void PushState(IStateBase state) {

            if (screenStack.Count != 0) {
                var previousState = screenStack.Peek();
                previousState.OnSendToBack();
                previousState.DisableRaycasts();
            }

            PushStateLocally(state);
        }

        public void OnUpdate(float deltaTime) {
            if(screenStack.Count == 0) {
                throw new NotSupportedException("Trying to call OnUpdate on the screenstack but it's empty!");
            }

            timerFactory.UpdateTimers(deltaTime);

            if (inputLocker.IsInputLocked || isLoading) {
                return;
            }

            var currentState = screenStack.Peek();
            currentState.OnUpdate();
        }

        private void PushStateLocally(IStateBase state) {

            isLoading = true;

            screenStack.Push(state);

            var stateEntry = statesCatalog.GetEntry(state.GetId());

            InstantiateViews(stateEntry, state);
        }

        private async void InstantiateViews(StateCatalogEntry stateEntry, IStateBase state) {

            var stateAssetLoader = assetLoaderFactory.CreateLoader(stateEntry.Id);

            stateAssetLoader.AddReference(stateEntry.WorldView);
            stateAssetLoader.AddReference(stateEntry.UiView);

            foreach(var stateAsset in stateEntry.StateAssets) {
                stateAssetLoader.AddReference(stateAsset);
            }

            await stateAssetLoader.LoadAsync();

            var uiViewAsset = stateAssetLoader.GetAsset<UiView>(stateEntry.UiView);
            var worldViewAsset = stateAssetLoader.GetAsset<WorldView>(stateEntry.WorldView);

            var stateAssetsList = new List<ScriptableObject>();

            foreach(var stateAsset in stateEntry.StateAssets) {
                stateAssetsList.Add(stateAssetLoader.GetAsset<ScriptableObject>(stateAsset));
            }

            state.CacheStateAssets(stateAssetsList);

            var worldView = UnityEngine.Object.Instantiate(worldViewAsset);
            var uiView = UnityEngine.Object.Instantiate(uiViewAsset);

            worldView.SetCanvasOrder((screenStack.Count - 1) * CanvasOffset);
            uiView.SetCanvasOrder((screenStack.Count - 1) * CanvasOffset + 5);

            state.LinkViews(uiView, worldView);

            if(state is IPreloadable preloadableState) {
                await preloadableState.Preload();
            }

            state.OnCreate();

            isLoading = false;
        }

        private void PopStateLocally() {
            var state = screenStack.Pop();
            state.ReleaseAssets(state.GetId());
            state.OnDestroy();
            state.DestroyViews();

            timerFactory.DestroyAllTimersFromState(state);

            if(screenStack.Count != 0) {
                var nextState = screenStack.Peek();
                nextState.OnBringToFront();
                nextState.EnableRaycasts();
            }
        }

        public LockHandle Lock() {
            var currentState = screenStack.Peek();
            inputLocker.Lock(currentState);
            return new LockHandle(inputLocker);
        }

    }

}