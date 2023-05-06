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

            model = new GameStateModel(context.Catalogs.LevelsCatalog, context.Catalogs.UnitsCatalog, context.Catalogs.TilesCatalog, 
                context.Catalogs.CommandersCatalog, context.Catalogs.ArmyColorsCatalog, gameStateArgs.LevelId);
            inputLocker = new GameplayInputLocker(context.ScreenMachine);
            unitHandler = new UnitHandler(inputLocker);
            unitHandler.OnUnitMovementStart += OnMovementStart;
            unitHandler.OnUnitMovementEnd += OnMovementEnd;
			buildingHandler = new BuildingHandler(context.Catalogs.ArmyColorsCatalog);

            CreatePlayers();
            CreateMapController();
            CreateCameraController();

			inputCalculatorHelper = new InputCalculatorHelper(mapController);
            inputCalculatorHelper.Init();

            tileHoverHandler = new TileHoverHandler(inputCalculatorHelper, mapController);

		}

        private void OnTileHover() {
            tileHoverHandler.OnHover();
        }

        private void CreatePlayers() {

            warController = new WarController(new BattleCalculatorHelper());

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
            warController.OnCreate();
        }

        private void CreateMapController() {

            var armyInfos = warController.GetArmyInfos();
            var levelProvider = new LevelProvider(context.Catalogs.LevelsCatalog, context.Catalogs.TilesCatalog);
            var tileMapModel = new TileMapModel(context.Catalogs.LevelsCatalog, context.Catalogs.MovementTypesCatalog, 
                context.Catalogs.TilesCatalog, context.Catalogs.UnitsCatalog, levelProvider,
                context.Catalogs.ArmyColorsCatalog, armyInfos, gameStateArgs.LevelId);
            mapController = new MapController(worldView.TileMapView, tileMapModel, unitHandler, buildingHandler);
            mapController.OnMapClicked += OnMapClicked;
            mapController.OnCreate();
        }

        private void OnMapClicked() {
            var tileClickedPosition = inputCalculatorHelper.GetTileFromMousePosition();
            var type = mapController.GetTypeFromTile(tileClickedPosition);
            var tileData = new TileData {
                Position = tileClickedPosition,
                TypeId = type
            };
            OnTileClicked(tileData);
        }


        //Ugliest method in the class, but it shouldnt grow much now
        private void OnTileClicked(TileData tileData)
        {
            //Almost all of this could be inside unit handler
            if (unitHandler.HasUnitSelected) {

                if (model.IsAttacking) {
                    TryAttackSelectedTile(tileData);
                } else {
					TryMoveSelectedUnit(tileData);
				}
                return;
            }

            var currentArmyId = warController.GetCurrentTurnArmyIndex();
            var isTileEmpty = unitHandler.IsSpaceEmpty(tileData.Position);

            if (!isTileEmpty)
            {
                if (unitHandler.IsFromArmy(tileData.Position, currentArmyId) && unitHandler.CanUnitMove(tileData.Position)) {
                    unitHandler.SetUnitSelected(tileData.Position);
                    mapController.HighlightAvailableTiles(tileData.Position, currentArmyId, unitHandler.GetSelectedUnitId());
                }
                return;
            }


            var isBuilding = model.IsBuilding(tileData.TypeId);
            if (!isBuilding) {
                return;
            }
            
            OnBuildingClicked(tileData);
        }

        private void TryAttackSelectedTile(TileData tileData) {
            var isEmpty = unitHandler.IsSpaceEmpty(tileData.Position);
            if (isEmpty) {
                return;
            }

            var currentArmy = warController.GetCurrentTurnArmyIndex();
            var isSameArmy = unitHandler.IsFromArmy(tileData.Position, currentArmy);
            if (isSameArmy) {
                return; //Can't attack your own units... for now..
            }

            var currentUnit = unitHandler.GetSelectedUnitId();
            var currentUnitPosition = unitHandler.GetSelectedUnitPosition();
            var currentUnitEntry = model.GetUnitCatalogEntry(currentUnit);
            var tilesInRange = mapController.GetTilesInRange(currentUnitPosition, currentUnitEntry.UnitSpecificationConfig);
            if (!tilesInRange.Contains(tileData.Position)) {
                return; //Not in range
            }

            //Check if units can be attacked maybe air, earth etc etc

            AttackSelectedTile(tileData);
		}

        private void AttackSelectedTile(TileData tileData) {
            var config = GetBattleConfig(tileData);
            var result = warController.SimulateBattle(config);
            ApplyBattleResult(result);
            model.IsAttacking = false;
            
 			var tilesInRange = mapController.GetTilesInRange(result.AttackerPosition, result.AttackerUnit.UnitSpecificationConfig);
			mapController.ClearAttackableTiles(tilesInRange);

            ExhaustCurrentUnit();
		}

        private void ApplyBattleResult(BattleConfiguration result) {
            //WIP
            if(result.DefenderHp == 0) {
                RemoveUnit(result.DefenderPosition);
			}
		}

        private BattleConfiguration GetBattleConfig(TileData tileData) {

			var currentUnit = unitHandler.GetSelectedUnitId();
			var currentUnitEntry = model.GetUnitCatalogEntry(currentUnit);

            var defenderUnit = unitHandler.GetUnitControllerAtPosition(tileData.Position);
            var defenderUnitEntry = model.GetUnitCatalogEntry(defenderUnit.GetUnitId());

            var attackerTileType = mapController.GetTypeFromTile(unitHandler.GetSelectedUnitPosition());
            var attackerTileEntry = model.GetTileEntryById(attackerTileType);

            var defenderTilEntry = model.GetTileEntryById(tileData.TypeId);

            return new BattleConfiguration {
                AttackerHp = 10000,
                DefenderHp = 10000,
                AttackerPosition = unitHandler.GetSelectedUnitPosition(),
                DefenderPosition = tileData.Position,
                AttackerUnit = currentUnitEntry,
                DefenderUnit = defenderUnitEntry,
                AttackerTile = attackerTileEntry,
                DefenderTile = defenderTilEntry
            };
        }

        private void TryMoveSelectedUnit(TileData tileData) {

			var gridPath = mapController.GetPath(tileData.Position);

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

			unitHandler.TryUnlockInput();

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

            var armyInfo = warController.GetArmyInfos().FirstOrDefault(x=>x.playerIndex == currentPlayer);
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

        private void RemoveUnit(Vector2Int position) { //TODO: Should be inside UnitHandler
            unitHandler.RemoveUnitAtPosition(position);
        }

        private void TryUnlockInput() {
            unitHandler.TryUnlockInput();
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