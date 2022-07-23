using Assets.Configs;
using Assets.Controllers;
using Assets.Data;
using Assets.Data.Level;
using Assets.Data.Levels;
using Assets.Data.Models;
using Assets.Views;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.States {
    public class GameStateController : BaseStateController<GameStateUiView, GameStateWorldView>, IStateBase {

        private const string Id = "GameState";

        private GameStateModel model;

        private MapController mapController;

        private CameraController cameraController;

        private WarController warController;

        private readonly GameStateArgs gameStateArgs;

        public GameStateController(Context context, GameStateArgs stateArgs) : base(context) {
            this.gameStateArgs = stateArgs;
        }

        public string GetId() {
            return Id;
        }

        public void OnBringToFront() {

        }

        public void OnCreate() {

            model = new GameStateModel(context.catalogs.LevelsCatalog, gameStateArgs.LevelId);

            CreatePlayers();
            CreateMapController();
            CreateCameraController();

        }

        private void CreatePlayers() {
            warController = new WarController();
            var totalPlayers = model.GetTotalPlayers();
            
            for(int i = 0; i< totalPlayers; ++i) {
                var fundsController = new FundsController();
                var playerView = uiView.InstantiatePlayerView();
                var playerController = new PlayerController(playerView, fundsController);
                warController.AddPlayer(playerController);
            }
        }

        private void CreateMapController() {
            var levelProvider = new LevelProvider(context.catalogs.LevelsCatalog);
            var tileMapModel = new TileMapModel(context.catalogs.LevelsCatalog, context.catalogs.TilesCatalog, context.catalogs.UnitsCatalog, 
                levelProvider, gameStateArgs.LevelId);
            mapController = new MapController(worldView.TileMapView, tileMapModel, new UnitHandler());
            mapController.OnBuildingClicked += PushPopupState;            
            mapController.CreateMap();
        }

        private void PushPopupState(TileData tileData) {
            var popupStateArgs = new PopupStateArgs { 
                OnUnitCreated = CreateUnit,
                TileTypeId = tileData.TypeId,
                Position = tileData.Position
            };
            
            PushState(new CreateUnitStateController(context, popupStateArgs));
        }

        private void CreateCameraController() {
            var cameraConfig = GetStateAsset<CameraConfig>();
            var cameraModel = new CameraModel(cameraConfig, context.catalogs.LevelsCatalog.GetAllEntries().First());
            cameraController = new CameraController(worldView.CameraView, cameraModel);
            cameraController.Init();
        }

        public void OnDestroy() {
            cameraController?.Destroy();
            mapController?.OnDestroy();
            mapController.OnBuildingClicked -= PushPopupState;
        }

        public void OnSendToBack() {

        }

        private void CreateUnit(BuyUnitData unitData) {
            mapController.CreateUnit(unitData);
        }

    }

}