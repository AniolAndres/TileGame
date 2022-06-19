using UnityEngine;
using System;
using UnityEngine.UI;

namespace Assets.Views {
    public class TileView : MonoBehaviour {

        public event Action OnTilePressed;

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

    }
}