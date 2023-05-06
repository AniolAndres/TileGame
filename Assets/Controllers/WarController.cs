
using Assets.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Controllers {
    public class WarController {

        private readonly List<PlayerController> players = new List<PlayerController>();

        private PlayerController currentPlayer; //The one currently playing the turn

        private readonly BattleCalculatorHelper battleCalculatorHelper;

        private List<ArmyInfoData> armyInfoList;

        public WarController(BattleCalculatorHelper battleCalculatorHelper) {
            this.battleCalculatorHelper = battleCalculatorHelper;
		}

        public void OnCreate() {
			armyInfoList = new List<ArmyInfoData>(players.Count);
			foreach (var player in players) {
				armyInfoList.Add(new ArmyInfoData {
					armyColorId = player.GetArmyColorId(),
					playerIndex = player.GetArmyIndex(),
					armyCommanderId = player.GetArmyCommanderId()
				});
			}
		}

        public void AddPlayer(PlayerController player) {
            players.Add(player);
        }

        public void SetInitialPlayer() {

            foreach(var player in players) {
                player.Hide(true);
            }

            currentPlayer = players.First();
            currentPlayer.OnTurnStart();
        }

        public void SetNextPlayer() {
            currentPlayer.OnTurnEnd();
            var index = players.IndexOf(currentPlayer);
            ++index;
            index = index > players.Count - 1 ? 0 : index;
            currentPlayer = players[index];
            currentPlayer.OnTurnStart();
        }

        public int GetCurrentTurnArmyIndex()
        {
            return currentPlayer.GetArmyIndex();
        }

        public int GetFundsFromCurrentPlayer()
        {
            return currentPlayer.GetCurrentFunds();
        }

        public void TakeFundsFromCurrentPlayer(int cost)
        {
            currentPlayer.TakeFundsFromPlayer(cost);
        }

        public List<ArmyInfoData> GetArmyInfos() {
            return armyInfoList;
        }

        public BattleConfiguration SimulateBattle(BattleConfiguration config) {
            return battleCalculatorHelper.SimulateBattle(config);
        }
    }
}