using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets.Views {
    public class UnitPurchaseView : MonoBehaviour, IPointerEnterHandler {

        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField]
        private TextMeshProUGUI costText;

        [SerializeField]
        private Image unitIcon;

        [SerializeField]
        private Button purchaseButton;

        public event Action OnClickView;

        public event Action OnHover;

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

        public void OnPointerEnter(PointerEventData eventData) {
            OnHover?.Invoke();
        }
    }
}