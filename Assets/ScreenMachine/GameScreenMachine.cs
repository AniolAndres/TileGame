using Assets.Catalogs.Scripts;
using System;
using System.Collections.Generic;

namespace Assets.ScreenMachine {

    public class GameScreenMachine : IScreenMachine {

        private Stack<IStateBase> screenStack;

        private StatesCatalog statesCatalog;

        private InputLocker inputLocker;

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

            if (inputLocker.IsInputLocked) {
                return;
            }

            var currentState = screenStack.Peek();
            currentState.OnUpdate();
        }

        private void PushStateLocally(IStateBase state) {

            screenStack.Push(state);

            var stateEntry = statesCatalog.GetEntry(state.GetId());

            InstantiateViews(stateEntry, state);

            state.OnCreate();
        }

        private void InstantiateViews(StateCatalogEntry stateEntry, IStateBase state) {
            var worldView = UnityEngine.Object.Instantiate(stateEntry.WorldView);
            var uiView = UnityEngine.Object.Instantiate(stateEntry.UiView);
            state.LinkViews(uiView, worldView);
        }

        private void PopStateLocally() {
            var state = screenStack.Peek();
            state.OnDestroy();
            state.DestroyViews();
            screenStack.Pop();

            if(screenStack.Count != 0) {
                var nextState = screenStack.Peek();
                nextState.OnBringToFront();
                nextState.EnableRaycasts();
            }
        }

        public T GetStateAsset<T>() {
            var state = screenStack.Peek();
            var entry = statesCatalog.GetEntry(state.GetId());

            return entry.GetStateAsset<T>();
        }

        public LockHandle Lock() {
            var currentState = screenStack.Peek();
            inputLocker.Lock(currentState);
            return new LockHandle(inputLocker);
        }
    }

}