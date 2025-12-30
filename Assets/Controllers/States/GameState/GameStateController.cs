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
using Assets.Catalogs;
using Assets.Catalogs.Scripts;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UIElements;

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
        
        private LevelEventDispatcher levelEventDispatcher;

        private InputCalculatorHelper inputCalculatorHelper;

        private TileHoverHandler tileHoverHandler;

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

            worldView.OnSecondaryButtonClick += OnSecondaryButtonClicked;
            worldView.OnMouseUpdate += OnTileHover;

            var levelProvider = new LevelProvider(context.Catalogs.LevelsCatalog);
            model = new GameStateModel(context.Catalogs.UnitsCatalog, context.Catalogs.TilesCatalog, 
                context.Catalogs.CommandersCatalog, context.Catalogs.ArmyColorsCatalog, gameStateArgs.LevelId, levelProvider);
            inputLocker = new GameplayInputLocker(context.ScreenMachine);
            unitHandler = new UnitHandler(inputLocker);
            unitHandler.OnUnitMovementStart += OnMovementStart;
            unitHandler.OnUnitMovementEnd += OnMovementEnd;
			buildingHandler = new BuildingHandler(context.Catalogs.ArmyColorsCatalog);


            var playerData = model.GetCurrentLevelPlayerData();
            CreatePlayers(playerData);
            
            var currentMap = model.GetCurrentLevelMapData();
            CreateMapController(currentMap);
            CreateCameraController(currentMap);

            levelEventDispatcher = new LevelEventDispatcher(warController, unitHandler);
            levelEventDispatcher.Init();
            SubscribeToLevelEvents();
            
			inputCalculatorHelper = new InputCalculatorHelper(mapController);
            inputCalculatorHelper.Init();

            tileHoverHandler = new TileHoverHandler(inputCalculatorHelper, mapController);
            
		}

        private void SubscribeToLevelEvents()
        {
            levelEventDispatcher.OnTurnStart += HandleTurnStart;
        }

        private void HandleTurnStart(int turnNumber, PlayerController playerController)
        {
            var armyName = playerController.GetArmyCommanderId();
            
            var startOverlayLock = inputLocker.LockInput();
            
            var turnStartArgs = new TurnStartOverlayStateArgs(turnNumber, armyName, () => startOverlayLock.Unlock());
            var turnStartState = new TurnStartOverlayStateController(context, turnStartArgs);
            
            PushState(turnStartState);
        }

        private void OnTileHover() {
            tileHoverHandler.OnHover();
        }

        private void CreatePlayers(PlayerData[] players){

            warController = new WarController(new BattleCalculatorHelper(), players);

            if (gameStateArgs.ArmyDatas != null)
            {
                throw new NotImplementedException("Not implemented having option to inject characters before level setup");
            }

            for (var i = 0; i < players.Length; i++)
            {
                PlayerData playerData = players[i];
                CommanderCatalogEntry commanderEntry = model.GetCommanderEntry(playerData.CommanderId);
                ArmyColorCatalogEntry armyEntry = model.GetArmyEntry(playerData.ColorId);
                FundsController fundsController = new FundsController();
                PlayerModel playerModel = new PlayerModel(commanderEntry, armyEntry, playerData.PlayerIndex, playerData.TeamId);
                PlayerView playerView = uiView.InstantiatePlayerView();
                var playerController = new PlayerController(playerView, playerModel, fundsController);
                playerController.OnCreate();
                warController.AddPlayer(playerController);
            }

            warController.SetInitialPlayer();
            warController.Init();
        }

        private void CreateMapController(MapData currentMap) {
            var armyInfos = warController.GetPlayersData();
            var tileMapModel = new TileMapModel(context.Catalogs.MovementTypesCatalog, 
                context.Catalogs.TilesCatalog, context.Catalogs.UnitsCatalog,
                context.Catalogs.ArmyColorsCatalog, armyInfos, currentMap);
            mapController = new MapController(worldView.TileMapView, tileMapModel, unitHandler, buildingHandler);
            mapController.OnMapClicked += OnMapClicked;
            mapController.OnCreate();
        }

        private void OnMapClicked() {

            var tileClickedPosition = inputCalculatorHelper.GetTileFromMousePosition();

            //On small maps the click can be out of bounds
            if (mapController.IsOutOfBounds(tileClickedPosition)) {
                return;
            }

            var type = mapController.GetTypeFromTile(tileClickedPosition);
            var tileData = new Tile {
                Position = tileClickedPosition,
                TypeId = type
            };
            OnTileClicked(tileData);
        }


        //Ugliest method in the class, but it shouldnt grow much now
        private void OnTileClicked(Tile tile)
        {
            //Almost all of this could be inside unit handler
            if (unitHandler.HasUnitSelected) {

                if (model.IsAttacking) {
                    TryAttackSelectedTile(tile);
                } else {
					TryMoveSelectedUnit(tile);
				}
                return;
            }

            var currentArmyId = warController.GetCurrentTurnArmyIndex();
            var isTileEmpty = unitHandler.IsSpaceEmpty(tile.Position);

            if (!isTileEmpty)
            {
                if (unitHandler.IsFromArmy(tile.Position, currentArmyId) && unitHandler.CanUnitMove(tile.Position)) {
                    unitHandler.SetUnitSelected(tile.Position);
                    mapController.HighlightAvailableTiles(tile.Position, currentArmyId, unitHandler.GetSelectedUnitId());
                }
                return;
            }


            var isBuilding = model.IsSpawnableBuilding(tile.TypeId);
            if (!isBuilding) {
                return;
            }
            
            OnBuildingClicked(tile);
        }

        private void TryAttackSelectedTile(Tile tile) {
            var isEmpty = unitHandler.IsSpaceEmpty(tile.Position);
            if (isEmpty) {
                return;
            }

            var currentArmy = warController.GetCurrentTurnArmyIndex();
            var isSameArmy = unitHandler.IsFromArmy(tile.Position, currentArmy);
            if (isSameArmy) {
                return; //Can't attack your own units... for now..
            }

            var currentUnit = unitHandler.GetSelectedUnitId();
            var currentUnitPosition = unitHandler.GetSelectedUnitPosition();
            var currentUnitEntry = model.GetUnitCatalogEntry(currentUnit);
            var tilesInRange = mapController.GetTilesInRange(currentUnitPosition, currentUnitEntry.UnitSpecificationConfig);
            if (!tilesInRange.Contains(tile.Position)) {
                return; //Not in range
            }

            //Check if units can be attacked maybe air, earth etc etc

            AttackSelectedTile(tile);
		}

        private void AttackSelectedTile(Tile tile) {
            var config = GetBattleConfig(tile);
            var result = warController.SimulateBattle(config);
            ApplyBattleResult(ref result);
            model.IsAttacking = false;
            
 			var tilesInRange = mapController.GetTilesInRange(result.AttackerPosition, result.AttackerUnit.UnitSpecificationConfig);
			mapController.ClearAttackableTiles(tilesInRange);

            ExhaustCurrentUnit();
		}

        private void ApplyBattleResult(ref BattleConfiguration result) {
            //WIP

            unitHandler.ApplyBattleResult(ref result);

		}

        private BattleConfiguration GetBattleConfig(Tile tile) {

			var attackerId = unitHandler.GetSelectedUnitId();
            var attackerPosition = unitHandler.GetSelectedUnitPosition();
            var attackerUnit = unitHandler.GetUnitControllerAtPosition(attackerPosition);
			var currentUnitEntry = model.GetUnitCatalogEntry(attackerId);


            var defenderUnit = unitHandler.GetUnitControllerAtPosition(tile.Position);
            var defenderUnitEntry = model.GetUnitCatalogEntry(defenderUnit.GetUnitId());

            var attackerTileType = mapController.GetTypeFromTile(unitHandler.GetSelectedUnitPosition());
            var attackerTileEntry = model.GetTileEntryById(attackerTileType);

            var defenderTilEntry = model.GetTileEntryById(tile.TypeId);

            return new BattleConfiguration {
                AttackerHp = attackerUnit.GetHp(),
                DefenderHp = defenderUnit.GetHp(),
                AttackerPosition = attackerPosition,
                DefenderPosition = tile.Position,
                AttackerUnit = currentUnitEntry,
                DefenderUnit = defenderUnitEntry,
                AttackerTile = attackerTileEntry,
                DefenderTile = defenderTilEntry
            };
        }

        private void TryMoveSelectedUnit(Tile tile) {

			var gridPath = mapController.GetPath(tile.Position);

			if (gridPath == null) {
				return;
			}

            MoveSelectedUnit(gridPath);
		}

        private void MoveSelectedUnit(List<Vector2Int> gridPath) {
			var listOfRealPositions = mapController.GetListOfRealPositions(gridPath);
            unitHandler.MoveSelectedUnit(gridPath, listOfRealPositions);
		}

		private void OnAttackSelected() {
            HighlightAttackableTiles();
            model.IsAttacking = true;
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
                OnUndoMove = OnCancelMovement,
                OnCaptureBuilding = OnCaptureBuilding
            };

            var postMovementState = new PostMovementMenuStateController(context, postMovementArgs);
            PushState(postMovementState);
        }

        private void OnCaptureBuilding() {
            var position = unitHandler.GetSelectedUnitPosition();
            var tileType = mapController.GetTypeFromTile(position);
            var isBuilding = model.IsBuilding(tileType);
            if (!isBuilding) {
                Debug.LogWarning($"It should not be possible to capture a non-building tile, current tile ({position.x}, {position.y})");
                return;
            }

            var currentPlayer = warController.GetCurrentTurnArmyIndex();
            var isBuildingFromCurrentPlayer = buildingHandler.IsBuildingFromPlayer(currentPlayer, position);
            if (isBuildingFromCurrentPlayer) {
				Debug.LogWarning($"It should not be possible to capture your own tile, current tile ({position.x}, {position.y})");
				return;
			}

            var armyInfo = warController.GetPlayersData().FirstOrDefault(x=>x.PlayerIndex == currentPlayer);
            if (armyInfo == null) {

                Debug.LogWarning($"Couldn't find an army info for player index: {currentPlayer}");
                return;
            }
			buildingHandler.ConvertBuildingToPlayer(armyInfo, position);
            ExhaustCurrentUnit();
        }


        private void ExhaustCurrentUnit() {
			unitHandler.ExhaustCurrentUnit();
			unitHandler.CleanLastMove();
			unitHandler.DeselectSelectedUnit();
		}

        private void OnMovementConfirmed() {
            ExhaustCurrentUnit();
		}

        private void OnCancelMovement() {
            var lastMoveData = unitHandler.GetLastMoveData();
            var realOriginPosition = mapController.GetRealTilePosition(lastMoveData.Origin);
            unitHandler.UndoLastMove(realOriginPosition);
            unitHandler.CleanLastMove();
            unitHandler.DeselectSelectedUnit();
            var type = mapController.GetTypeFromTile(lastMoveData.Origin);
            OnTileClicked(new Tile { //simulate selecting the unit again
                TypeId = type,
                Position = lastMoveData.Origin
            });
		}

        private void OnBuildingClicked(Tile tile)
        {
            var currentPlayer = warController.GetCurrentTurnArmyIndex();
            var isFromPlayer = buildingHandler.IsBuildingFromPlayer(currentPlayer,tile.Position);
            if (!isFromPlayer)
            {
                return;
            }
            
            PushPopupState(tile);
        }

        private void PushPopupState(Tile tile) {
            var popupStateArgs = new CreateUnitStateArgs { 
                OnUnitCreated = CreateUnit,
                CurrentFunds = warController.GetFundsFromCurrentPlayer(),
                TileTypeId = tile.TypeId,
                Position = tile.Position
            };
            
            PushState(new CreateUnitStateController(context, popupStateArgs));
        }

        private void CreateCameraController(MapData mapData) {
            var cameraConfig = GetStateAsset<CameraConfig>();
            var cameraModel = new CameraModel(cameraConfig, mapData);
            cameraController = new CameraController(worldView.CameraView, cameraModel);
            cameraController.Init();
        }

        public void OnDestroy() {
            cameraController.Destroy();
			mapController.OnMapClicked -= OnMapClicked;
			mapController.OnDestroy();
			worldView.OnSecondaryButtonClick -= OnSecondaryButtonClicked;
            worldView.OnMouseUpdate -= OnTileHover;
			unitHandler.OnUnitMovementStart += OnMovementStart;
			unitHandler.OnUnitMovementEnd += OnMovementEnd;
		}

        private void OnMovementEnd() {
            PushPostMovementState();
		}

        private void OnMovementStart() {
            ClearPathFinding();
		}

        public void OnSendToBack() {

        }

        private void CreateUnit(BuyUnitData unitData)
        {
            var currentArmyId = warController.GetCurrentTurnArmyIndex();
            var unitEntry = model.GetUnitCatalogEntry(unitData.UnitId);
            warController.TakeFundsFromCurrentPlayer(unitEntry.UnitSpecificationConfig.Cost);
            var unitModel = new UnitModel(unitEntry, currentArmyId);
            var unitMapView = mapController.CreateUnitView(unitEntry, unitData);

            unitHandler.AddUnit(unitMapView,unitModel, unitData.Position);
        }

        private void ClearPathFinding() {
            mapController.ClearCurrentPathfinding();
        }


        private void OnSecondaryButtonClicked() {

            if (unitHandler.HasUnitSelected) {
                if (model.IsAttacking) {
                    model.IsAttacking = false;
					var currentUnitPosition = unitHandler.GetSelectedUnitPosition();
                    var currentUnitEntry = model.GetUnitCatalogEntry(unitHandler.GetSelectedUnitId());
					var tilesInRange = mapController.GetTilesInRange(currentUnitPosition, currentUnitEntry.UnitSpecificationConfig);
					mapController.ClearAttackableTiles(tilesInRange);
					PushPostMovementState();
                } else {
					unitHandler.DeselectSelectedUnit();
					mapController.ClearCurrentPathfinding();
				}
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
            unitHandler.DeselectSelectedUnit();
        }
    }

}