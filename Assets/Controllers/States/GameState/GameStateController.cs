using Assets.Configs;
using Assets.Controllers.States;
using Assets.Data;
using Assets.Data.Level;
using Assets.Data.Levels;
using Assets.Data.Models;
using Assets.ScreenMachine;
using Assets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Controllers {
    public class GameStateController : BaseStateController<GameStateUiView, GameStateWorldView>, IStateBase {

        private const string Id = "GameState";

        private GameStateModel model;

        private MapController mapController;

        private UnitHandler unitHandler;

        private BuildingHandler buildingHandler;

        private GameplayInputLocker inputLocker;

        private CameraController cameraController;

        private WarController warController;

        private InputCalculatorHelper inputCalculatorHelper;

        private readonly GameStateArgs gameStateArgs;

        private GameStateStateMachine gameStateStateMachine;

        public GameStateController(Context context, GameStateArgs stateArgs) : base(context) {
            this.gameStateArgs = stateArgs;
        }

        public string GetId() {
            return Id;
        }

        public void OnBringToFront() {

        }

        public void OnCreate() {

            uiView.OnSecondaryButtonClick += OnSecondaryButtonClicked;

            model = new GameStateModel(context.Catalogs.LevelsCatalog, context.Catalogs.UnitsCatalog, context.Catalogs.TilesCatalog, 
                context.Catalogs.CommandersCatalog, context.Catalogs.ArmyColorsCatalog, gameStateArgs.LevelId);
            inputLocker = new GameplayInputLocker(context.ScreenMachine);
            unitHandler = new UnitHandler(inputLocker);
            unitHandler.OnMovementStop += PushPostMovementState;
			buildingHandler = new BuildingHandler();

            CreatePlayers();
            CreateMapController();
            CreateCameraController();

            CreateGameplayStateMachine();

			inputCalculatorHelper = new InputCalculatorHelper(mapController);
            inputCalculatorHelper.Init();
		}

        private void CreateGameplayStateMachine() {

            var gameplayContext = new GameplayContext(warController, mapController, unitHandler, buildingHandler, context.Catalogs);
            gameStateStateMachine = new GameStateStateMachine(gameplayContext);
            gameStateStateMachine.OnCreateUnitPushRequested += PushCreateUnitMenuState;
			gameStateStateMachine.OnPostMovementRequested += PushPostMovementState;
            gameStateStateMachine.Initialize();
		}

        private void PushCreateUnitMenuState(TileData tileData) {
			var popupStateArgs = new CreateUnitStateArgs {
				OnUnitCreated = CreateUnit,
				CurrentFunds = warController.GetFundsFromCurrentPlayer(),
				TileTypeId = tileData.TypeId,
				Position = tileData.Position
			};

			PushState(new CreateUnitStateController(context, popupStateArgs));
		}

        private void CreatePlayers() {
            warController = new WarController();

            for (var armyIndex = 0; armyIndex < gameStateArgs.ArmyDatas.Count; armyIndex++)
            {
                var armyData = gameStateArgs.ArmyDatas[armyIndex];
                var commanderEntry = model.GetCommanderEntry(armyData);
                var armyEntry = model.GetArmyEntry(armyData);
                var fundsController = new FundsController();
                var playerModel = new PlayerModel(commanderEntry, armyEntry);
                var playerView = uiView.InstantiatePlayerView();
                var playerController = new PlayerController(playerView, playerModel, fundsController);
                playerController.OnCreate();
                warController.AddPlayer(playerController);
            }

            warController.SetInitialPlayer();
        }

        private void CreateMapController() {

            var armyInfos = warController.GetArmyInfos();
            var levelProvider = new LevelProvider(context.Catalogs.LevelsCatalog, context.Catalogs.TilesCatalog);
            var tileMapModel = new TileMapModel(context.Catalogs.LevelsCatalog, context.Catalogs.MovementTypesCatalog, 
                context.Catalogs.TilesCatalog, context.Catalogs.UnitsCatalog, levelProvider,
                context.Catalogs.ArmyColorsCatalog, armyInfos, gameStateArgs.LevelId);
            mapController = new MapController(worldView.TileMapView, tileMapModel, unitHandler, buildingHandler);
            //mapController.OnTileClicked += OnTileClicked;
            mapController.OnMapClicked += OnMapClicked;
            mapController.OnCreate();
        }

        private void OnMapClicked() {
            var tileClickedPosition = inputCalculatorHelper.GetTileClicked();
            var type = mapController.GetTypeFromTile(tileClickedPosition);
            var tileData = new TileData {
                Position = tileClickedPosition,
                TypeId = type
            };
            OnTileClicked(tileData);
        }

        private void OnTileClicked(TileData tileData)
        {
            gameStateStateMachine.OnTileClicked(tileData);


            ////Almost all of this could be inside unit handler
            //if (unitHandler.HasUnitSelected) {

            //    TryMoveSelectedUnit(tileData);

            //    return;
            //}

            //var currentArmyId = warController.GetCurrentTurnArmyIndex();
            //var isTileEmpty = unitHandler.IsSpaceEmpty(tileData.Position);

            //if (!isTileEmpty)
            //{
            //    if (unitHandler.IsFromArmy(tileData.Position, currentArmyId) && unitHandler.CanUnitMove(tileData.Position)) {
            //        unitHandler.SetUnitSelected(tileData.Position);
            //        mapController.HighlightAvailableTiles(tileData.Position, currentArmyId, unitHandler.GetSelectedUnitId());
            //    }
            //    return;
            //}


            //var isBuilding = model.IsBuilding(tileData.TypeId);
            //if (!isBuilding) {
            //    return;
            //}
            
            //OnBuildingClicked(tileData);
        }

		private void OnAttackSelected() {
            HighlightAttackableTiles();
        }

        private void HighlightAttackableTiles() {
            var unit = unitHandler.GetSelectedUnitId();
            var position = unitHandler.GetSelectedUnitPosition();
            var unitEntry = model.GetUnitCatalogEntry(unit);
            var tilesInRange = mapController.GetTilesInRange(position, unitEntry.UnitSpecificationConfig);
            mapController.HighlightTilesInRange(tilesInRange);
        }

        private void PushPostMovementState() {

			var postMovementArgs = new PostMovementMenuStateArgs {
                CanAttack = true,
                OnAttack = OnAttackSelected,
                OnMovementConfirmed = OnMovementConfirmed,
                OnUndoMove = OnCancelMovement
            };

            var postMovementState = new PostMovementMenuStateController(context, postMovementArgs);
            PushState(postMovementState);
        }

        private void OnMovementConfirmed() {           
            unitHandler.ExhaustCurrentUnit();
            unitHandler.CleanLastMove();
            unitHandler.DeselectSelectedUnit();
        }

        private void OnCancelMovement() {
            var lastMoveData = unitHandler.GetLastMoveData();
            var realOriginPosition = mapController.GetRealTilePosition(lastMoveData.Origin);
            unitHandler.UndoLastMove(realOriginPosition);
            unitHandler.CleanLastMove();
            unitHandler.DeselectSelectedUnit();
            var type = mapController.GetTypeFromTile(lastMoveData.Origin);
            OnTileClicked(new TileData { //simulate selecting the unit again
                TypeId = type,
                Position = lastMoveData.Origin
            });
		}

        private void OnBuildingClicked(TileData tileData)
        {
            var currentPlayer = warController.GetCurrentTurnArmyIndex();
            var isFromPlayer = buildingHandler.IsBuildingFromPlayer(currentPlayer,tileData.Position);
            if (!isFromPlayer)
            {
                return;
            }
            
            PushPopupState(tileData);
        }

        private void PushPopupState(TileData tileData) {
            var popupStateArgs = new CreateUnitStateArgs { 
                OnUnitCreated = CreateUnit,
                CurrentFunds = warController.GetFundsFromCurrentPlayer(),
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
			mapController.OnMapClicked -= OnMapClicked;
			mapController.OnDestroy();
			uiView.OnSecondaryButtonClick -= OnSecondaryButtonClicked;
            gameStateStateMachine.OnCreateUnitPushRequested -= PushCreateUnitMenuState;
            gameStateStateMachine.OnPostMovementRequested -= PushPostMovementState;
            gameStateStateMachine.OnDestroy();
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
            var currentArmyId = warController.GetCurrentTurnArmyIndex();
            var unitEntry = model.GetUnitCatalogEntry(unitData.UnitId);
            warController.TakeFundsFromCurrentPlayer(unitEntry.UnitSpecificationConfig.Cost);
            var unitModel = new UnitModel(unitEntry, currentArmyId);
            var unitMapView = mapController.CreateUnit(unitEntry, unitData);
            var unitController = new UnitController(unitMapView, unitModel); //This is wrong, state shouldn't be dealing with all units, this should be delegated to unitHandler
            unitController.OnCreate();
            unitHandler.AddUnit(unitController, unitData.Position);
        }

        private void ClearPathFinding() {
            mapController.ClearCurrentPathfinding();
        }


        private void OnSecondaryButtonClicked() {

            if (unitHandler.HasUnitSelected) {

                unitHandler.DeselectSelectedUnit();
                mapController.ClearCurrentPathfinding();
                return;
            }


            var battleInfoMenuStateArgs = new BattleInfoMenuStateArgs {
                OnOptionClicked = EndTurn,
            };
            var battleInfoMenuController = new BattleInfoMenuStateController(context, battleInfoMenuStateArgs);

            PushState(battleInfoMenuController);
        }

        private void EndTurn() {
            warController.SetNextPlayer();
            var nextPlayerIndex = warController.GetCurrentTurnArmyIndex();
            unitHandler.RefreshAllUnitsFromArmy(nextPlayerIndex);
        }
    }

}