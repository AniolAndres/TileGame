using Assets.Configs;
using Assets.Controllers;
using Assets.Data;
using Assets.Data.Level;
using Assets.Data.Levels;
using Assets.Data.Models;
using Assets.Views;
using System.Linq;
using UnityEngine;

namespace Assets.States {
    public class GameState : BaseState<GameStateUiView, GameStateWorldView>, IStateBase {

        private const string Id = "GameState";

        private GameStateModel model;

        private MapController mapController;

        private CameraController cameraController;

        public GameState(Context context) : base(context) { }

        public string GetId() {
            return Id;
        }

        public void OnBringToFront() {

        }

        public void OnCreate() {

            model = new GameStateModel();

            CreateMapController();
            CreateCameraController();

        }

        private void CreateMapController() {
            var levelProvider = new LevelProvider(context.catalogs.LevelsCatalog);
            var tileMapModel = new TileMapModel(context.catalogs.LevelsCatalog, context.catalogs.TilesCatalog, context.catalogs.UnitsCatalog, levelProvider);
            mapController = new MapController(uiView.TileMapView, tileMapModel, new UnitHandler());
            mapController.OnTerrainClicked += PushPopupState;            
            mapController.CreateMap();
        }

        private void PushPopupState(TileData tileData) {
            var popupStateArgs = new PopupStateArgs { 
                OnUnitCreated = CreateUnit,
                Position = tileData.Position
            };
            
            PushState(new PopupState(context, popupStateArgs));
        }

        private void CreateCameraController() {
            var cameraConfig = GetStateAsset<CameraConfig>();
            var cameraModel = new CameraModel(cameraConfig, context.catalogs.LevelsCatalog.GetAllEntries().First());
            cameraController = new CameraController(uiView.CameraView, cameraModel);
            cameraController.Init();
        }

        public void OnDestroy() {
            cameraController?.Destroy();
            mapController?.OnDestroy();
            mapController.OnTerrainClicked -= PushPopupState;
        }

        public void OnSendToBack() {

        }

        private void CreateUnit(BuyUnitData unitData) {
            mapController.CreateUnit(unitData);
        }

    }

}