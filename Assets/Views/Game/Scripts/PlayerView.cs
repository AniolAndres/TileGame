using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Views {
    public class PlayerView : MonoBehaviour {

        [SerializeField]
        private GameObject root;

        [SerializeField]
        private Image backgroundImage;

        [SerializeField]
        private Image playerIcon;

        [SerializeField]
        private TextMeshProUGUI playerNameText;

        [SerializeField]
        private TextMeshProUGUI fundsText;

        public void Setup(ref CommanderViewData uiData) {
            playerNameText.text = uiData.FullName;
            backgroundImage.color = uiData.Color;
        }

        public void Show(bool instant) {
            root.SetActive(true);
        }

        public void Hide(bool instant) {
            root.SetActive(false);
        }

        public void UpdateCount(int newBalance)
        {
            fundsText.text = newBalance.ToString();
        }
    }
}