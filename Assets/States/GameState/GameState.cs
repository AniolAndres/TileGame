using Assets.Controllers;
using Assets.States.GameState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : BaseState, IStateBase
{
    private GameStateUiView uiView;

    private GameStateWorldView worldView;

    private const string Id = "GameState";

    private GameStateModel model = new GameStateModel();

    private MapController mapController;

    public GameState(IScreenMachine screenMachine) : base(screenMachine) { }

    public string GetId() {
        return Id;
    }

    public void OnBringToFront() {

    }

    public void OnCreate() {
        mapController = new MapController();
        mapController.CreateMap(10,10);

    }

    private void PopState() {
        screenMachine.PopState();
    }

    public void OnDestroy() {
    }

    public void OnSendToBack() {

    }

    public void LinkViews(UiView uiView, WorldView worldView) {
        this.uiView = uiView as GameStateUiView;
        this.worldView = worldView as GameStateWorldView;
    }

    public void DestroyViews() {
        Object.Destroy(uiView.gameObject);
        Object.Destroy(worldView.gameObject);
    }
}
