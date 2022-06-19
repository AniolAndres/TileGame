using Assets.Configs;
using Assets.Controllers;
using Assets.Data.Models;
using Assets.Views;
using System.Linq;

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
            var tileMapModel = new TileMapModel(context.catalogs.LevelsCatalog);
            mapController = new MapController(uiView.TileMapView, tileMapModel);
            mapController.CreateMap();
        }

        private void CreateCameraController() {
            var cameraConfig = GetStateAsset<CameraConfig>();
            var cameraModel = new CameraModel(cameraConfig, context.catalogs.LevelsCatalog.GetAllEntries().First());
            cameraController = new CameraController(uiView.CameraView, cameraModel);
            cameraController.Init();
        }

        private void PopState() {
            screenMachine.PopState();
        }

        public void OnDestroy() {
            cameraController?.Destroy();
        }

        public void OnSendToBack() {

        }

    }

}