using Assets.Catalogs.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMachine : IScreenMachine
{

    private Stack<IStateBase> screenStack;

    private StatesCatalog statesCatalog;

    public void Init(StatesCatalog statesCatalog) {
        screenStack = new Stack<IStateBase>();
        this.statesCatalog = statesCatalog;
    }

    public void PopState() {
        PopStateLocally();
    }

    public void PresentState(IStateBase state) {

        while(screenStack.Count != 0) {
            PopStateLocally();
        }

        PushStateLocally(state);
    }

    public void PushState(IStateBase state) {

        if(screenStack.Count != 0) {
            var previousState = screenStack.Peek();
            previousState.OnSendToBack();
        }

        PushStateLocally(state);
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
    }
}
