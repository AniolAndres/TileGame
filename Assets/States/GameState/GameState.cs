using Assets.Catalogs.Scripts;
using Assets.Controllers;
using Assets.Data.Models;
using Assets.Views;
using UnityEngine;

namespace Assets.States {
    public class GameState : BaseState, IStateBase {

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
            var config = GetStateAsset<PlaceholderGameConfig>();
            Debug.Log($"Fetching config, value is {config.Value}");
            var model = new TileMapModel(new Vector2Int(10, 10));
            mapController = new MapController(uiView.TileMapView, model);
            mapController.CreateMap();

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

}