
using Assets.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Configs;

namespace Assets.Controllers {
    public class WarController {

        private readonly List<PlayerController> players = new List<PlayerController>();

        private PlayerController currentPlayer; //The one currently playing the turn

        private readonly BattleCalculatorHelper battleCalculatorHelper;

        private int turnNumber = 0;

        private PlayerData[] playersData;

        public event Action<int, PlayerController> OnTurnStart;
        public event Action<int> OnRoundStart;

        public WarController(BattleCalculatorHelper battleCalculatorHelper, PlayerData[] playersData) {
            this.battleCalculatorHelper = battleCalculatorHelper;
            this.playersData = playersData;
		}

        public void Init() {
            turnNumber = 1;
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
            
            OnTurnStart?.Invoke(turnNumber, currentPlayer);
        }

        public void SetNextPlayer() {
            currentPlayer.OnTurnEnd();
            var index = players.IndexOf(currentPlayer);
            ++index;
            
            var isNewRound = index > players.Count - 1;
            
            index = isNewRound ? 0 : index;
            currentPlayer = players[index];
            currentPlayer.OnTurnStart();

            if (isNewRound)
            {
                turnNumber++;
                OnRoundStart?.Invoke(turnNumber);
            }
            
            OnTurnStart?.Invoke(turnNumber, currentPlayer);
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

        public PlayerData[] GetPlayersData() {
            return playersData;
        }

        public BattleConfiguration SimulateBattle(BattleConfiguration config) {
            return battleCalculatorHelper.SimulateBattle(config);
        }
    }
}