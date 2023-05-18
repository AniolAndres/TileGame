using Assets.Data;
using Assets.Data.Models;
using Assets.ScreenMachine;
using Assets.Views;
using System.Collections;
using UnityEngine;

namespace Assets.Controllers {
	public class DialogStateController : BaseStateController<DialogStateUiView, DialogStateWorldView>, IStateBase {

		private readonly DialogStateArgs stateArgs;

		private readonly DialogStateModel model;

		private const string Id = "DialogState";

		public DialogStateController(Context context, DialogStateArgs stateArgs) : base(context) {
			this.stateArgs = stateArgs;
		}

		public string GetId() {
			return Id;
		}

		public void OnBringToFront() {
			
		}

		public void OnCreate() {
			
		}

		public void OnDestroy() {
			
		}

		public void OnSendToBack() {
			
		}
	}
}