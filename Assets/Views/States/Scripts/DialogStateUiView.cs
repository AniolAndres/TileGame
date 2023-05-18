using Assets.ScreenMachine;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Views {
	public class DialogStateUiView : UiView
	{
		[SerializeField]
		private Transform topParent;

		[SerializeField]
		private Transform bottomParent;

		public event Action OnContinuePressed;

		public event Action OnSkip;

		public override void OnUpdate() {

			if (Input.GetKey(KeyCode.Return)) {
				OnContinuePressed?.Invoke();
				return;
			}

			if (Input.GetKey(KeyCode.Escape)) {
				OnSkip?.Invoke();
			}
		}
	}
}