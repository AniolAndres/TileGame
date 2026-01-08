using System;

namespace Assets.Controllers.States
{
    public class TurnStartOverlayStateArgs
    {
        public int RoundNumber { get; private set; }

		public string PlayerName { get; private set; }
        

        public TurnStartOverlayStateArgs(int roundNumber, string playerName)
        {
            RoundNumber = roundNumber;
            PlayerName = playerName;
        }
    }
}