using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace Assets.Views {
    public class BattleInfoOptionView : MonoBehaviour {

        [SerializeField]
        private Button button;

        public event Action OnClicked;

        private void OnEnable() {
            button.onClick.AddListener(FireClickAction);            
        }

        private void FireClickAction() {
            OnClicked?.Invoke();
        }

        private void OnDisable() {
            button.onClick.RemoveListener(FireClickAction);
        }
    }
}