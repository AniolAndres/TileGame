using Assets.ScreenMachine;
using UnityEngine;
using System;

namespace Assets.Views {
    public class BattleInfoMenuStateUiView : UiView {

        [SerializeField]
        private BattleInfoOptionView optionPrefab;

        [SerializeField]
        private Transform optionsParent;

        public event Action OnPresentRequested;

        public event Action OnPopRequested;

        public event Action OnPushRequested;

        public event Action OnOptionClicked;


        public override void OnUpdate() {
            base.OnUpdate();

            if (Input.GetMouseButtonDown(1)) {
                OnPopRequested?.Invoke();
            }
        }

        public void InstantiateOptions() {

            for(int i = 0; i < 3; ++i) {
                var optionView = Instantiate(optionPrefab, optionsParent);
                optionView.OnClicked += OnAnyOptionClicked; //Move to controllers etc etc
            }
        }

        public void OnAnyOptionClicked() {
            OnOptionClicked?.Invoke();
        }

    }

}