using Assets.Catalogs.Scripts;
using Assets.Views;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ScreenMachine {

    public class GameScreenMachine : IScreenMachine {

        private Stack<IStateBase> screenStack;

        private StatesCatalog statesCatalog;

        private InputLocker inputLocker;

        private IAssetLoader screenMachineAssetLoader = new AssetLoader();

        private bool isLoading;

        public void Init(StatesCatalog statesCatalog) {
            screenStack = new Stack<IStateBase>();
            inputLocker = new InputLocker();
            this.statesCatalog = statesCatalog;
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

        public void OnUpdate() {
            if(screenStack.Count == 0) {
                throw new NotSupportedException("Trying to call OnUpdate on the screenstack but it's empty!");
            }

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
            screenMachineAssetLoader.AddPrefabReference(stateEntry.WorldView);
            screenMachineAssetLoader.AddPrefabReference(stateEntry.UiView);

            foreach(var stateAsset in stateEntry.StateAssets) {
                screenMachineAssetLoader.AddScriptableObjectReference(stateAsset);
            }

            await screenMachineAssetLoader.LoadAsync();

            var uiViewAsset = screenMachineAssetLoader.GetPrefabAsset<UiView>(stateEntry.UiView);
            var worldViewAsset = screenMachineAssetLoader.GetPrefabAsset<WorldView>(stateEntry.WorldView);

            var stateAssetsList = new List<ScriptableObject>();

            foreach(var stateAsset in stateEntry.StateAssets) {
                stateAssetsList.Add(screenMachineAssetLoader.GetScriptableObject(stateAsset));
            }

            state.CacheStateAssets(stateAssetsList);

            var uiView = UnityEngine.Object.Instantiate(uiViewAsset);
            var worldView = UnityEngine.Object.Instantiate(worldViewAsset);

            state.LinkViews(uiView, worldView);

            state.OnCreate();

            isLoading = false;
        }

        private void PopStateLocally() {
            var state = screenStack.Peek();
            var stateEntry = statesCatalog.GetEntry(state.GetId());
            screenMachineAssetLoader.DisposeStateLoadedAssets(stateEntry);
            state.OnDestroy();
            state.DestroyViews();
            screenStack.Pop();

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