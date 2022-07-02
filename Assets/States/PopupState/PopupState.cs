
using Assets.Catalogs;
using Assets.Catalogs.Scripts;
using Assets.Configs;
using Assets.Controllers;
using Assets.Data;
using Assets.Data.Models;
using Assets.Views;
using Assets.Views.States.Scripts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.States {
    public class PopupState : BaseState<PopupStateUiView, PopupStateWorldView>, IStateBase {

        private const string Id = "PopupState";

        private PopupStateModel model;

        private List<PurchaseTileController> tileControllerList = new List<PurchaseTileController>();

        public PopupState(Context context) : base(context) { }

        public string GetId() {
            return Id;
        }

        public void OnBringToFront() {

        }

        public void OnCreate() {
            uiView.OnPopRequested += PopState;

            var popupStateConfig = GetStateAsset<PopupStateConfig>();
            model = new PopupStateModel(context.catalogs.UnitsCatalog, popupStateConfig);

            CreatePurchaseControllers();
        }

        private void CreatePurchaseControllers() {

            var units = model.GetUnits();
            var unitPrefab = model.GetUnitPrefab();

            foreach(var unit in units) {
                var unitPurchaseViewData = GetUnitPurchaseViewData(unit.UnitPurchaseViewConfig);
                var unitPurchaseView = uiView.InstantiatePurchaseView(unitPrefab, ref unitPurchaseViewData);               
                var unitPurchaseModel = new UnitPurchaseModel(unit);
                var unitController = new PurchaseTileController(unitPurchaseView, unitPurchaseModel);
                unitController.OnCreate();
                unitController.OnUnitBought += CreateUnitAndPopState;
                tileControllerList.Add(unitController);
            }

            UnitPurchaseViewData GetUnitPurchaseViewData(UnitPurchaseViewConfig unitViewConfig) {
                return new UnitPurchaseViewData {
                    Name = unitViewConfig.NameKey,
                    Cost = unitViewConfig.Cost.ToString(),
                    UnitIcon = unitViewConfig.UnitSprite
                };
            }
        }

        private void CreateUnitAndPopState(string unitId) {
            var unitEntry = model.GetUnitEntry(unitId);
            CreateUnit(unitEntry);

            PopState();
        }

        private void CreateUnit(UnitCatalogEntry unitEntry) {
            Debug.Log($"Creating unit {unitEntry.Id}");
        }

        public void OnDestroy() {
            foreach(var tileController in tileControllerList) {
                tileController.OnUnitBought -= CreateUnitAndPopState;
                tileController.OnDestroy();
            }

            uiView.OnPopRequested -= PopState;
        }

        public void OnSendToBack() {

        }

    }

}