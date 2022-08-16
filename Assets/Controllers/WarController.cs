using System.Collections.Generic;
using System.Linq;

namespace Assets.Controllers {
    public class WarController {

        private readonly List<PlayerController> players = new List<PlayerController>();

        private PlayerController currentPlayer; //The one currently playing the turn

        public void AddPlayer(PlayerController player) {
            players.Add(player);
        }

        public void SetInitialPlayer() {

            foreach(var player in players) {
                player.Hide(true);
            }

            currentPlayer = players.First();
            currentPlayer.Show(true);
        }

        public void SetNextPlayer() {
            currentPlayer.OnTurnEnd();
            var index = players.IndexOf(currentPlayer);
            ++index;
            index = index > players.Count - 1 ? 0 : index;
            currentPlayer = players[index];
            currentPlayer.OnTurnStart();
        }
    }
}