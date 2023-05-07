using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Assets.Views.ViewData;

namespace Assets.Views {
    public class TileView : MonoBehaviour {

        [SerializeField]
        private GameObject highlightGameObject;

        [SerializeField]
        private GameObject inAttackRangeHighlight;

        [SerializeField]
        private TextMeshProUGUI costText;

        [SerializeField]
        private GameObject mainBuildingTextGO;

        [SerializeField]
        private Image tileImage;

        [SerializeField]
        private Image ownerImage;

        [SerializeField]
        private Transform arrowTransform;

        public void Highlight(int cost, float arrowRotation) {
            highlightGameObject.SetActive(true);
            //costText.text = cost.ToString();
            //arrowTransform.localEulerAngles = new Vector3(0,0,arrowRotation);
        }

        public void Setup(Color tileColor, bool isMain) {
            tileImage.color = tileColor;
            mainBuildingTextGO.SetActive(isMain);
		}

        public void RemoveHighlight() {
            highlightGameObject.SetActive(false);
        }

        public void SetOwnerColor(Color armyColor) {
            ownerImage.gameObject.SetActive(true);
            ownerImage.color = armyColor;
        }

		public void SetState(TileState state) {
            inAttackRangeHighlight.SetActive(state == TileState.InRangeForAttack);
            highlightGameObject.SetActive(state == TileState.InRangeForMovement);
		}
	}
}