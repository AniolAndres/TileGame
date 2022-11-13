using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

namespace Assets.Views {
    public class TileView : MonoBehaviour {

        public event Action OnTilePressed;

        [SerializeField]
        private GameObject highlightGameObject;

        [SerializeField]
        private TextMeshProUGUI costText;

        [SerializeField]
        private Button tileButton;

        private void OnEnable() {
            tileButton.onClick.AddListener(FireTileClicked);
        }

        private void FireTileClicked() {
            OnTilePressed?.Invoke();
        }

        private void OnDisable() {
            tileButton.onClick.RemoveListener(FireTileClicked);
        }

        public void Highlight(int cost) {
            highlightGameObject.SetActive(true);
            costText.text = cost.ToString();
        }
    }
}