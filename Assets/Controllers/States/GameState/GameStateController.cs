using Assets.Configs;
using Assets.Data;
using Assets.Data.Level;
using Assets.Data.Levels;
using Assets.Data.Models;
using Assets.ScreenMachine;
using Assets.Views;
using System.Linq;

namespace Assets.Controllers {
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

            uiView.OnBattleInfoMenuRequested += PushBattleInfoMenu;

            model = new GameStateModel(context.Catalogs.LevelsCatalog, context.Catalogs.CommandersCatalog, gameStateArgs.LevelId);

            CreatePlayers();
            CreateMapController();
            CreateCameraController();

        }

        private void CreatePlayers() {
            warController = new WarController();

            foreach(var commanderId in gameStateArgs.CommanderIds) {
                var commanderEntry = model.GetCommanderEntry(commanderId);
                var fundsController = new FundsController();
                var playerModel = new PlayerModel(commanderEntry);
                var playerView = uiView.InstantiatePlayerView();
                var playerController = new PlayerController(playerView, playerModel, fundsController);
                playerController.OnCreate();
                warController.AddPlayer(playerController);
            }

            warController.SetInitialPlayer();
        }

        private void CreateMapController() {
            var levelProvider = new LevelProvider(context.Catalogs.LevelsCatalog);
            var tileMapModel = new TileMapModel(context.Catalogs.LevelsCatalog, context.Catalogs.TilesCatalog, context.Catalogs.UnitsCatalog, 
                levelProvider, gameStateArgs.LevelId);
            var inputLocker = new GameplayInputLocker(context.ScreenMachine);
            var unitHandler = new UnitHandler(inputLocker);
            mapController = new MapController(worldView.TileMapView, tileMapModel, unitHandler);
            mapController.OnBuildingClicked += PushPopupState;            
            mapController.CreateMap();
        }

        private void PushPopupState(TileData tileData) {
            var popupStateArgs = new CreateUnitStateArgs { 
                OnUnitCreated = CreateUnit,
                TileTypeId = tileData.TypeId,
                Position = tileData.Position
            };
            
            PushState(new CreateUnitStateController(context, popupStateArgs));
        }

        private void CreateCameraController() {
            var cameraConfig = GetStateAsset<CameraConfig>();
            var cameraModel = new CameraModel(cameraConfig, context.Catalogs.LevelsCatalog.GetAllEntries().First());
            cameraController = new CameraController(worldView.CameraView, cameraModel);
            cameraController.Init();
        }

        public void OnDestroy() {
            cameraController?.Destroy();
            mapController?.OnDestroy();
            mapController.OnBuildingClicked -= PushPopupState;
            uiView.OnBattleInfoMenuRequested -= PushBattleInfoMenu;
        }

        public void OnSendToBack() {

        }

        private void CreateUnit(BuyUnitData unitData) {
            mapController.CreateUnit(unitData);
        }


        private void PushBattleInfoMenu() {
            var battleInfoMenuStateArgs = new BattleInfoMenuStateArgs {
                OnOptionClicked = EndTurn,
            };
            var battleInfoMenuController = new BattleInfoMenuStateController(context, battleInfoMenuStateArgs);

            PushState(battleInfoMenuController);
        }

        private void EndTurn() {
            warController.SetNextPlayer();
        }
    }

}