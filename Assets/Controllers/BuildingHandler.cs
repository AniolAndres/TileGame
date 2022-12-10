
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controllers {
    public class BuildingHandler {

        private readonly Dictionary<int, Dictionary<Vector2Int, TileController>> playerBuildings = new Dictionary<int, Dictionary<Vector2Int, TileController>>();

        private Dictionary<Vector2Int, TileController> totalBuildings = new Dictionary<Vector2Int, TileController>();

        public List<Vector2Int> GetBuildingsFromPlayer(int playerIndex) {
            var buildings = playerBuildings[playerIndex];
            return buildings;
        }

        public int GetFundsGainedFromPlayer(int playerIndex) {
            var buildings = playerBuildings[playerIndex];

            var amount = 0;
            foreach (var building in buildings) {
                var gain = building.Value.GetFunds();
                amount += gain;
            }

            return amount;
        }
    }
}