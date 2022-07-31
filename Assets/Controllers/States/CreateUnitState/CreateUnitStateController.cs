
using Assets.Catalogs;
using Assets.Catalogs.Scripts;
using Assets.Data;
using Assets.Data.Models;
using Assets.ScreenMachine;
using Assets.Views;
using Assets.Views.States.Scripts;
using System.Collections.Generic;

namespace Assets.Controllers {
    public class CreateUnitStateController : BaseStateController<CreateUnitStateUiView, CreateUnitStateWorldView>, IStateBase {

        private const string Id = "CreateUnit";

        private CreateUnitStateModel model;

        private List<PurchaseTileController> tileControllerList = new List<PurchaseTileController>();

        private readonly CreateUnitStateArgs stateArgs;

        public CreateUnitStateController(Context context, CreateUnitStateArgs args) : base(context) {
            stateArgs = args;
        }

        public string GetId() {
            return Id;
        }

        public void OnBringToFront() {

        }

        public void OnCreate() {
            uiView.OnPopRequested += PopState;

            var popupStateConfig = GetStateAsset<CreateUnitStateConfig>();
            model = new CreateUnitStateModel(context.Catalogs.UnitsCatalog, context.Catalogs.TilesCatalog, popupStateConfig);

            CreatePurchaseControllers();
        }

        public void OnSendToBack() {

        }

        private void CreatePurchaseControllers() {

            var units = model.GetUnits(stateArgs.TileTypeId);
            var unitPrefab = model.GetUnitPrefab();

            foreach(var unit in units) {
                var unitPurchaseViewData = GetUnitPurchaseViewData(unit);
                var unitPurchaseView = uiView.InstantiatePurchaseView(unitPrefab, ref unitPurchaseViewData);               
                var unitPurchaseModel = new UnitPurchaseModel(unit);
                var unitController = new PurchaseTileController(unitPurchaseView, unitPurchaseModel);
                unitController.OnCreate();
                unitController.OnUnitHovered += ChangeDisplayedInfo;
                unitController.OnUnitBought += CreateUnitAndPopState;
                tileControllerList.Add(unitController);
            }

            //----------------------

            UnitPurchaseViewData GetUnitPurchaseViewData(UnitCatalogEntry unit) {
                return new UnitPurchaseViewData {
                    Name = unit.UnitPurchaseViewConfig.NameKey,
                    Cost = unit.UnitSpecificationConfig.Cost.ToString(),
                    UnitIcon = unit.UnitPurchaseViewConfig.UnitSprite
                };
            }
        }

        public void OnDestroy() {
            foreach(var tileController in tileControllerList) {
                tileController.OnUnitBought -= CreateUnitAndPopState;
                tileController.OnUnitHovered -= ChangeDisplayedInfo;
                tileController.OnDestroy();
            }

            uiView.OnPopRequested -= PopState;
        }

        private void ChangeDisplayedInfo(string unitId) {
            var unitEntry = model.GetUnitEntry(unitId);
            var infoData = new PurchaseInfoViewData {
                UnitIcon = unitEntry.UnitPurchaseViewConfig.FullBodySprite,
                AttackValue = unitEntry.UnitSpecificationConfig.Attack.ToString(),
                VisionValue = unitEntry.UnitSpecificationConfig.Vision.ToString(),
                MovementValue = unitEntry.UnitSpecificationConfig.Movemement.ToString()
            };
            uiView.RefreshInfoPanel(ref infoData);
        }

        private void CreateUnitAndPopState(string unitId) {
            var unitEntry = model.GetUnitEntry(unitId);
            PopState();
            CreateUnit(unitEntry);
        }

        private void CreateUnit(UnitCatalogEntry unitEntry) {
            var buyData = new BuyUnitData {
                UnitId = unitEntry.Id,
                Position = stateArgs.Position
            };
            stateArgs.OnUnitCreated?.Invoke(buyData);
        }
    }

}