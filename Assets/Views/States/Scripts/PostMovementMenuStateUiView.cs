using Assets.ScreenMachine;
using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Extensions;

namespace Assets.Views {
    public class PostMovementMenuStateUiView : UiView {

        [SerializeField]
        private Button attackButton;

        [SerializeField]
        private Button moveButton;

        [SerializeField]
        private Button cancelButton;

        [SerializeField]
        private Button captureButton;

        [SerializeField]
        private RectTransform layoutTransform;

        public event Action OnCancel;

        public event Action OnWait;

        public event Action OnAttack;

        public event Action OnUndoMove;

        public event Action OnCapture;

        public override void OnUpdate() {
            if (Input.GetMouseButtonDown(1)) {
                FireCancelEvent();
            }
        }

        private void OnEnable() {
            attackButton.onClick.AddListener(FireAttackEvent);
            moveButton.onClick.AddListener(FireMoveEvent);
            cancelButton.onClick.AddListener(FireCancelEvent);
            captureButton.onClick.AddListener(FireCaptureEvent);
        }

        private void OnDisable() {
            attackButton.onClick.RemoveListener(FireAttackEvent);
            moveButton.onClick.RemoveListener(FireMoveEvent);
            cancelButton.onClick.RemoveListener(FireCancelEvent);
			captureButton.onClick.RemoveListener(FireCaptureEvent);
		}

        private void FireCaptureEvent() {
            OnCapture?.Invoke();
        }

        private void FireCancelEvent() {
			OnUndoMove?.Invoke();
        }

        private void FireMoveEvent() {
            OnWait?.Invoke();
        }

        private void FireAttackEvent() {
            OnAttack?.Invoke();
        }

        public void Setup(bool canAttack) {

			attackButton.gameObject.SetActive(canAttack);
			LayoutRebuilder.ForceRebuildLayoutImmediate(layoutTransform); //Needed otherwise it doesn't properly recalculate

			var mouseRelativePosition = GetMouseQuarterScreenPosition();

			layoutTransform.anchoredPosition = Input.mousePosition.ToVector2();
            layoutTransform.anchoredPosition -= mouseRelativePosition * layoutTransform.sizeDelta;
        }

        private Vector2 GetMouseQuarterScreenPosition() {
            var mousePosition = Input.mousePosition.ToVector2();
            var relativePosition = mousePosition / new Vector2(Screen.width, Screen.height);
            return new Vector2(Mathf.Round(relativePosition.x), Mathf.Round(relativePosition.y));
        }
    }
}