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

		[SerializeField]
		private DialogSampleView dialogViewPrefab;

		public event Action OnContinuePressed;

		public event Action OnSkip;

		//Need a pool of dialogs, for the future
		private DialogSampleView cachedDialog;

		public override void OnUpdate() {

			if (Input.GetKeyDown(KeyCode.Return)) {
				CompleteOrContinue();
				return;
			}

			if (Input.GetKeyDown(KeyCode.Escape)) {
				OnSkip?.Invoke();
			}
		}

		private void CompleteOrContinue() {
			if (cachedDialog.IsFinished) {
				OnContinuePressed?.Invoke();
				return;
			}

			cachedDialog.Complete();
		}

		public void DisplayMessage(DialogViewData viewData) {

			var parent = viewData.IsTop ? topParent : bottomParent;	

			cachedDialog ??= Instantiate(dialogViewPrefab); //If there's only one there's no need to instantiate, change later

			cachedDialog.transform.SetParent(parent, false);

			cachedDialog.SetMessage(viewData.Message);
		}
	}
}