
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controllers {
    public class BuildingHandler {

        private readonly Dictionary<int, Dictionary<Vector2Int, TileController>> totalBuildings = new Dictionary<int, Dictionary<Vector2Int, TileController>>();

        public Dictionary<Vector2Int, TileController> GetBuildingsFromPlayer(int playerIndex) {
            var buildings = totalBuildings[playerIndex];
            return buildings;
        }

        public int GetFundsGainedFromPlayer(int playerIndex) {
            var buildings = totalBuildings[playerIndex];
            var amount = 0;
            foreach (var building in buildings) {
                var gain = building.Value.GetFunds();
                amount += gain;
            }

            return amount;
        }

        public bool IsBuildingFromPlayer(int playerIndex, Vector2Int buildingPosition) {
            var playerBuildings = GetBuildingsFromPlayer(playerIndex);
            return playerBuildings.ContainsKey(buildingPosition);
        }

        private int GetBuildingOwner(Vector2Int buildingPosition) {
            foreach (var playerBuildings in totalBuildings) {
                if (playerBuildings.Value.ContainsKey(buildingPosition)){
                    return playerBuildings.Key;
                }
            }

            throw new ArgumentException($"Couldn't find any building in the array with position {buildingPosition}, check level initialization");
        }

        public void ConvertBuildingToPlayer(int playerIndex, Vector2Int buildingPosition) {

            var buildingOwner = GetBuildingOwner(buildingPosition);
            if(buildingOwner == playerIndex) {
                throw new NotSupportedException($"Building in position {buildingPosition} already belongs to player, check if it belongs to him first");
            }

            var controller = totalBuildings[buildingOwner];
            totalBuildings[buildingOwner].Remove(buildingPosition);
            totalBuildings[playerIndex] = controller;
        }

        public void AddBuilding(int playerIndex, Vector2Int position, TileController tileController) {
            if (!totalBuildings.ContainsKey(playerIndex)) {
                totalBuildings[playerIndex] = new Dictionary<Vector2Int, TileController>();
            }

            totalBuildings[playerIndex][position] = tileController;
        }
	}
}