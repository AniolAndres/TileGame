using Assets.ScreenMachine;
using TMPro;
using UnityEngine;

namespace Assets.Views
{
    public class TurnStartOverlayUiView : UiView
    {
        [SerializeField] 
        private TextMeshProUGUI roundDescription;

        public void Setup(int roundNumber, string playerName)
        {
            roundDescription.SetText($"{playerName} - Round {roundNumber}".ToUpperInvariant());
        }
    }
}