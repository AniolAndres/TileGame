using UnityEngine;
using Assets.Catalogs;
using System.Linq;
using Assets.Views;
using Assets.Data.Levels;
using Assets.Data.Level;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Assets.Catalogs.Scripts;
using Assets.Configs;

namespace Assets.Data.Models {
    public class TileMapModel {

        private readonly LevelCatalogEntry currentLevelEntry;

        private readonly TilesCatalog tilesCatalog;

        private readonly UnitsCatalog unitsCatalog;

        private readonly ArmyColorsCatalog armyColorsCatalog;

        private readonly ILevelProvider levelProvider;

        private readonly MovementTypesCatalog movementTypesCatalog;

        private List<ArmyInfoData> armyInfoData;

        public MovementTypesCatalog MovementTypesCatalog => movementTypesCatalog;

        private readonly string levelId;

        public TileMapModel(LevelsCatalog levelsCatalog, MovementTypesCatalog movementTypesCatalog, TilesCatalog tilesCatalog, 
            UnitsCatalog unitsCatalog,ILevelProvider levelProvider, ArmyColorsCatalog armyColorsCatalog, List<ArmyInfoData> armyInfoData, string levelId) {           
            this.currentLevelEntry = levelsCatalog.GetAllEntries().First();
            this.tilesCatalog = tilesCatalog;
            this.movementTypesCatalog = movementTypesCatalog;
            this.unitsCatalog = unitsCatalog;
            this.levelProvider = levelProvider;
            this.levelId = levelId;
            this.armyColorsCatalog = armyColorsCatalog;
            this.armyInfoData = armyInfoData;
        }

        public Vector2Int GetSize() {
            return currentLevelEntry.Size;
        }

        public SerializableLevelData GetLevelData() {
            return levelProvider.GetLevel(levelId);
        }

        public Vector2 GetRealTileWorldPosition(Vector2Int tilePosition) {
            return new Vector2((tilePosition.x - currentLevelEntry.Size.x/2f + 0.5f) * currentLevelEntry.TileSideLength, 
                (tilePosition.y - currentLevelEntry.Size.y/2f + 0.5f) * currentLevelEntry.TileSideLength);
        }

        public float GetSideLength() {
            return currentLevelEntry.TileSideLength;
        }

        public TileCatalogEntry GetTileEntry(string typeId) {
            return tilesCatalog.GetEntry(typeId);
        }

        public UnitCatalogEntry GetUnitCatalogEntry(string unitId) {
            return unitsCatalog.GetEntry(unitId);
        }

        public bool IsBuilding(string tileType) {
            var tileEntry = tilesCatalog.GetEntry(tileType);
            return tileEntry.CanBeControlled;
        }

        public bool IsBuilding(TileCatalogEntry tileEntry) {
            return tileEntry.CanBeControlled;
        }

        public ArmyInfoData GetArmyInfo(int playerIndex) {
            
            if (playerIndex == 0) {
                return new ArmyInfoData {
                    playerIndex = 0,
                    armyColorId = armyColorsCatalog.InactiveColorEntry.Id,
                    armyCommanderId = null
                };
            }

            foreach(var armyInfo in armyInfoData) {
                if(armyInfo.playerIndex == playerIndex) {
                    return armyInfo;
                }
            }

            throw new NotSupportedException("Couldn't find any army data, check the algorithm");
        }

        public Color GetColor(string colorId) {
            var colorEntry = armyColorsCatalog.GetEntry(colorId);
            return colorEntry.ArmyColor;
        }
    }
}