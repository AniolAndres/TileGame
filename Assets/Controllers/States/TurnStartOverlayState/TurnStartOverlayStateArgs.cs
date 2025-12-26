using System;

namespace Assets.Controllers.States
{
    public class TurnStartOverlayStateArgs
    {
        public int RoundNumber { get; private set; }

		public string PlayerName { get; private set; }

        public Action OnStatePop; //Should be something generic

        public TurnStartOverlayStateArgs(int roundNumber, string playerName, Action onStatePop)
        {
            RoundNumber = roundNumber;
            PlayerName = playerName;
            OnStatePop = onStatePop;
        }
    }
}