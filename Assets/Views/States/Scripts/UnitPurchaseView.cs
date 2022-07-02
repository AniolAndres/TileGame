using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

namespace Assets.Views {
    public class UnitPurchaseView : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField]
        private TextMeshProUGUI costText;

        [SerializeField]
        private Image unitIcon;

        [SerializeField]
        private Button purchaseButton;

        public event Action OnClickView;

        public void Setup(ref UnitPurchaseViewData unitPurchaseViewData) {
            nameText.text = unitPurchaseViewData.Name;
            costText.text = unitPurchaseViewData.Cost;
            unitIcon.sprite = unitPurchaseViewData.UnitIcon;
        }

        private void OnEnable() {
            purchaseButton.onClick.AddListener(FireUnitClickedAction);
        }

        private void OnDisable() {
            purchaseButton.onClick.RemoveListener(FireUnitClickedAction);
        }

        private void FireUnitClickedAction() {
            OnClickView?.Invoke();
        }
    }
}