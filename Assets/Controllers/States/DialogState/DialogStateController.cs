using Assets.Data;
using Assets.Data.Models;
using Assets.ScreenMachine;
using Assets.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controllers {
	public class DialogStateController : BaseStateController<DialogStateUiView, DialogStateWorldView>, IStateBase {

		private readonly DialogStateArgs stateArgs;

		private DialogStateModel model;

		private const string Id = "DialogState";

		public DialogStateController(Context context, DialogStateArgs stateArgs) : base(context) {
			this.stateArgs = stateArgs;
		}

		public string GetId() {
			return Id;
		}

		public void OnCreate() {

			uiView.OnContinuePressed += Continue;
			uiView.OnSkip += Skip;

			model = new DialogStateModel(stateArgs.DialogData);

			var viewData = model.GetCurrentDialogViewData();
			uiView.DisplayMessage(viewData);
		}

		private void Skip() {
			PopState();
		}

		private void Continue() {
			model.AdvanceDialog();

			if (model.IsDialogFinished()) {
				Skip();
				return;
			}

			var currentDialog = model.GetCurrentDialogViewData();
			uiView.DisplayMessage(currentDialog);
		}

		public void OnDestroy() {
			uiView.OnContinuePressed -= Continue;
			uiView.OnSkip -= Skip;

		}

		public void OnBringToFront() {

		}

		public void OnSendToBack() {
			
		}
	}
}