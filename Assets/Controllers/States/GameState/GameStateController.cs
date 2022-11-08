using Assets.Configs;
using Assets.Data;
using Assets.Data.Level;
using Assets.Data.Levels;
using Assets.Data.Models;
using Assets.ScreenMachine;
using Assets.Views;
using System.Linq;
using UnityEngine;

namespace Assets.Controllers {
    public class GameStateController : BaseStateController<GameStateUiView, GameStateWorldView>, IStateBase {

        private const string Id = "GameState";

        private GameStateModel model;

        private MapController mapController;

        private UnitHandler unitHandler;

        private GameplayInputLocker inputLocker;

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

            model = new GameStateModel(context.Catalogs.LevelsCatalog, context.Catalogs.UnitsCatalog, context.Catalogs.TilesCatalog, context.Catalogs.CommandersCatalog, gameStateArgs.LevelId);
            inputLocker = new GameplayInputLocker(context.ScreenMachine);
            unitHandler = new UnitHandler(inputLocker);

            CreatePlayers();
            CreateMapController();
            CreateCameraController();

        }

        private void CreatePlayers() {
            warController = new WarController();

            for (var i = 0; i < gameStateArgs.CommanderIds.Count; i++)
            {
                var commanderId = gameStateArgs.CommanderIds[i];
                var commanderEntry = model.GetCommanderEntry(commanderId);
                var fundsController = new FundsController();
                var armyId = $"ArmyId{i}";
                var playerModel = new PlayerModel(commanderEntry, armyId);
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
            mapController = new MapController(worldView.TileMapView, tileMapModel);
            mapController.OnTileClicked += OnTileClicked;
            mapController.CreateMap();
        }

        private void OnTileClicked(TileData tileData)
        {
            //Almost all of this could be inside unit handler
            if (unitHandler.HasUnitSelected) {
                var realNewPosition = mapController.GetRealTilePosition(tileData.Position);
                unitHandler.MoveSelectedUnit(tileData.Position, realNewPosition);
                return;
            }

            var currentArmyId = warController.GetCurrentTurnArmyId();
            if (!unitHandler.IsSpaceEmpty(tileData.Position) && unitHandler.IsFromArmy(tileData.Position,currentArmyId)) {
                unitHandler.SetUnitSelected(tileData.Position);
                return;
            }

            var isBuilding = model.IsBuilding(tileData.TypeId);
            if (!isBuilding) {
                return;
            }
            
            OnBuildingClicked(tileData);
        }

        private void OnBuildingClicked(TileData tileData)
        {
            var isFromPlayer = model.DoesBuildingBelongToPlayer(tileData);
            if (!isFromPlayer)
            {
                return;
            }
            
            PushPopupState(tileData);
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
            cameraController.Destroy();
            mapController.OnDestroy();
            uiView.OnBattleInfoMenuRequested -= PushBattleInfoMenu;
        }

        public void OnSendToBack() {

        }

        private void RemoveUnit(Vector2Int position) {
            var removedController = unitHandler.GetUnitControllerAtPosition(position);
            removedController.OnDestroy();
            unitHandler.RemoveUnitAtPosition(position);
        }
        
        private void CreateUnit(BuyUnitData unitData)
        {
            var currentArmyId = warController.GetCurrentTurnArmyId();
            var unitEntry = model.GetUnitCatalogEntry(unitData.UnitId);
            var unitModel = new UnitModel(unitEntry, currentArmyId);
            var unitMapView = mapController.CreateUnit(unitEntry, unitData);
            unitMapView.OnMovementEnd += unitHandler.TryUnlockInput;
            var unitController = new UnitController(unitMapView, unitModel);
            unitHandler.AddUnit(unitController, unitData.Position);
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
            unitHandler.DeselectSelectedUnit();
        }
    }

}