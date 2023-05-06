using Assets.Data;
using Assets.ScreenMachine;
using Assets.Views;
using System;

namespace Assets.Controllers.States {
    public class PostMovementMenuStateController : BaseStateController<PostMovementMenuStateUiView, PostMovementMenuStateWorldView>, IStateBase {

        private const string Id = "PostMovementMenu";
        
        private readonly PostMovementMenuStateArgs stateArgs;

        public PostMovementMenuStateController(Context context, PostMovementMenuStateArgs stateArgs) : base(context) {
            this.stateArgs = stateArgs;
        }

        public string GetId() {
            return Id;
        }

        public void OnBringToFront() {
            
        }

        public void OnCreate() {

            uiView.Setup(stateArgs.CanAttack);

            uiView.OnAttack += OnAttack;
            uiView.OnUndoMove += OnUndoMovement;
            uiView.OnWait += OnConfirmMovement;
            uiView.OnCapture += OnCaptureBuilding;
        }

        private void OnCaptureBuilding() {
			PopState();
            stateArgs.OnCaptureBuilding?.Invoke();
		}

        private void OnAttack() {
			PopState();
			stateArgs.OnAttack?.Invoke();
		}

        public void OnDestroy() {
			uiView.OnAttack -= OnAttack;
			uiView.OnUndoMove -= OnUndoMovement;
            uiView.OnWait -= OnConfirmMovement;
			uiView.OnCapture -= OnCaptureBuilding;

		}

		private void OnUndoMovement() {
            PopState();
            stateArgs.OnUndoMove?.Invoke();
        }

        private void OnConfirmMovement() {
			PopState();
			stateArgs.OnMovementConfirmed?.Invoke();
		}

        public void OnSendToBack() {
            
        }

    }
}