using UnityEngine;
using Assets.Catalogs;
using System;
using System.Collections.Generic;
using Assets.Catalogs.Scripts;
using Assets.Configs;

namespace Assets.Data.Models {
    public class TileMapModel {

        private readonly MapData mapData;
        
        private readonly TilesCatalog tilesCatalog;

        private readonly UnitsCatalog unitsCatalog;

        private readonly ArmyColorsCatalog armyColorsCatalog;

        private readonly MovementTypesCatalog movementTypesCatalog;

        private readonly PlayerData[] playersData;

        public MovementTypesCatalog MovementTypesCatalog => movementTypesCatalog;

        public TileMapModel(MovementTypesCatalog movementTypesCatalog, TilesCatalog tilesCatalog, 
            UnitsCatalog unitsCatalog, ArmyColorsCatalog armyColorsCatalog, PlayerData[] playersData,
            MapData mapData)
        {
            this.mapData = mapData;
			this.tilesCatalog = tilesCatalog;
            this.movementTypesCatalog = movementTypesCatalog;
            this.unitsCatalog = unitsCatalog;
            this.armyColorsCatalog = armyColorsCatalog;
            this.playersData = playersData;
        }

        public Vector2Int GetSize() {
            return new Vector2Int(mapData.Width, mapData.Height);
        }

        public Vector2 GetRealTileWorldPosition(Vector2Int tilePosition) {
            var size = new Vector2Int(mapData.Width, mapData.Height);
            var tileSizeLength = mapData.TileSideLength;
			return new Vector2((tilePosition.x - size.x/2f + 0.5f) * tileSizeLength, 
                (tilePosition.y - size.y/2f + 0.5f) * tileSizeLength);
        }

        public float GetSideLength() {
            return mapData.TileSideLength;
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

        public PlayerData GetPlayerData(int playerIndex)
        {
            foreach(var playerData in playersData) {
                if(playerData.PlayerIndex == playerIndex) {
                    return playerData;
                }
            }

            throw new NotSupportedException("Couldn't find any army data, check the algorithm");
        }

        public Color GetColor(string colorId) {
            var colorEntry = armyColorsCatalog.GetEntry(colorId);
            return colorEntry.ArmyColor;
        }

        public MapData GetMapData()
        {
            return mapData;
        }
    }
}