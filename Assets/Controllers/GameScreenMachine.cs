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

        const int CanvasOffset = 30;

        private readonly Queue<IStateBase> statesToCleanUp = new Queue<IStateBase>();


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
            CleanStatesViews();
        }

        private void CleanStatesViews() {
            while(statesToCleanUp.Count > 0) {
                var state = statesToCleanUp.Dequeue();
                state.DestroyViews();
            }
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

            if (stateEntry.WorldView.RuntimeKeyIsValid()) {
                stateAssetLoader.AddReference(stateEntry.WorldView);
            }

            if (stateEntry.UiView.RuntimeKeyIsValid()) {
                stateAssetLoader.AddReference(stateEntry.UiView);
            }

            foreach(var stateAsset in stateEntry.StateAssets) {
                stateAssetLoader.AddReference(stateAsset);
            }

            await stateAssetLoader.LoadAsync();

            var uiViewAsset = stateEntry.UiView.RuntimeKeyIsValid() ? stateAssetLoader.GetAsset<UiView>(stateEntry.UiView) : null;
            var worldViewAsset = stateEntry.WorldView.RuntimeKeyIsValid() ? stateAssetLoader.GetAsset<WorldView>(stateEntry.WorldView) : null;

            var stateAssetsList = new List<ScriptableObject>();

            foreach(var stateAsset in stateEntry.StateAssets) {
                stateAssetsList.Add(stateAssetLoader.GetAsset<ScriptableObject>(stateAsset));
            }

            state.CacheStateAssets(stateAssetsList);

            var worldView = worldViewAsset != null ? UnityEngine.Object.Instantiate(worldViewAsset) : null;
            var uiView = uiViewAsset != null ? UnityEngine.Object.Instantiate(uiViewAsset) : null;

            if(worldView == null && uiView == null) {
                throw new NotSupportedException($"Both views of state {stateEntry.Id} are null! Did you forget to reference them?");
            }

            worldView?.SetCanvasOrder((screenStack.Count - 1) * CanvasOffset);
            uiView?.SetCanvasOrder((screenStack.Count - 1) * CanvasOffset + Mathf.RoundToInt(CanvasOffset/2));

            state.LinkViews(uiView, worldView);

            if(state is IPreloadable preloadableState) {
                await preloadableState.Preload();
            }

            state.OnCreate();

            isLoading = false;

            CleanStatesViews();
        }

        private void PopStateLocally() {
            var state = screenStack.Pop();
            state.ReleaseAssets(state.GetId());
            state.OnDestroy();
            statesToCleanUp.Enqueue(state);

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