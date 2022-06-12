using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace Assets.Views {
    public class TileView : MonoBehaviour {

        private Vector2Int position;

        public event Action<Vector2Int> OnTilePressed;

        [SerializeField]
        private Button tileButton;

        public void SetPosition(Vector2Int position) {
            this.position = position;
        }

        private void OnEnable() {
            tileButton.onClick.AddListener(FireTileClicked);
        }

        private void FireTileClicked() {
            OnTilePressed?.Invoke(position);
        }

        private void OnDisable() {
            tileButton.onClick.RemoveListener(FireTileClicked);
        }

    }
}