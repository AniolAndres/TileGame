using UnityEngine;
using TMPro;

namespace Assets.Views {
    public class UnitStatsView : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI visionValueText;
        [SerializeField]
        private TextMeshProUGUI movementValueText;
        [SerializeField]
        private TextMeshProUGUI attackValueText;
        
        public void SetUp(ref PurchaseInfoViewData infoViewData) {
            attackValueText.text = infoViewData.AttackValue;
            visionValueText.text = infoViewData.VisionValue;
            movementValueText.text = infoViewData.MovementValue;
        }

    }
}