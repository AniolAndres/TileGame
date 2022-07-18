using System.Collections.Generic;

namespace Assets.Controllers {
    public class WarController {

        private readonly List<PlayerController> players = new List<PlayerController>();

        private PlayerController currentPlayer; //The one currently playing the turn

        public void AddPlayer(PlayerController player) {
            players.Add(player);
        }

        public void SetNextPlayer() {
            currentPlayer.OnTurnEnd();
            var index = players.IndexOf(currentPlayer);
            index = index == players.Count - 1 ? 0 : index;
            currentPlayer = players[index];
            currentPlayer.OnTurnStart();
        }
    }
}