using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

namespace Assets.Views {
    public class TileView : MonoBehaviour {

        [SerializeField]
        private GameObject highlightGameObject;

        [SerializeField]
        private TextMeshProUGUI costText;

        [SerializeField]
        private Image tileImage;

        [SerializeField]
        private Image ownerImage;

        [SerializeField]
        private Transform arrowTransform;

        public void Highlight(int cost, float arrowRotation) {
            highlightGameObject.SetActive(true);
            costText.text = cost.ToString();
            arrowTransform.localEulerAngles = new Vector3(0,0,arrowRotation);
        }

        public void Setup(Color tileColor) {
            tileImage.color = tileColor;
        }

        public void RemoveHighlight() {
            highlightGameObject.SetActive(false);
        }

        public void SetOwnerColor(Color armyColor) {
            ownerImage.gameObject.SetActive(true);
            ownerImage.color = armyColor;
        }
    }
}