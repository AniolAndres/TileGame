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

        [SerializeField]
        private Image tileImage;

        [SerializeField]
        private Transform arrowTransform;

        private void OnEnable() {
            tileButton.onClick.AddListener(FireTileClicked);
        }

        private void FireTileClicked() {
            OnTilePressed?.Invoke();
        }

        private void OnDisable() {
            tileButton.onClick.RemoveListener(FireTileClicked);
        }

        public void Highlight(int cost, float arrowRotation) {
            highlightGameObject.SetActive(true);
            costText.text = cost.ToString();
            arrowTransform.localEulerAngles = new Vector3(0,0,arrowRotation);
        }

        internal void SetColor(Color tileColor) {
            tileImage.color = tileColor;
        }

        public void RemoveHighlight() {
            highlightGameObject.SetActive(false);
        }
    }
}