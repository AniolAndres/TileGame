using Assets.Catalogs.Scripts;
using Assets.Controllers;
using Assets.Data.Levels;
using Assets.Data.Models;
using Assets.Views;
using UnityEngine;

namespace Assets.States {
    public class GameState : BaseState<GameStateUiView, GameStateWorldView>, IStateBase {

        private const string Id = "GameState";

        private GameStateModel model;

        private MapController mapController;

        public GameState(Context context) : base(context) { }

        public string GetId() {
            return Id;
        }

        public void OnBringToFront() {

        }

        public void OnCreate() {

            var levelProvider = new LevelProvider(context.catalogs.LevelsCatalog);
            model = new GameStateModel(levelProvider);

            var tileMapModel = new TileMapModel(new Vector2Int(10, 10));
            mapController = new MapController(uiView.TileMapView, tileMapModel);
            mapController.CreateMap();

        }

        private void PopState() {
            screenMachine.PopState();
        }

        public void OnDestroy() {
        }

        public void OnSendToBack() {

        }

    }

}