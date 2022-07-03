using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.Views {
    public class PurchaseInfoView : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI infoText;

        [SerializeField]
        private Image unitIcon;

        [SerializeField]
        private UnitStatsView statsView;

        public void SetUp(ref PurchaseInfoViewData viewData) {
            unitIcon.sprite = viewData.UnitIcon;
            statsView.SetUp(ref viewData);
        }

    }
}