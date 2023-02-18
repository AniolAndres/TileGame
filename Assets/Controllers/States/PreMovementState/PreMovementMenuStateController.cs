using Assets.Data;
using Assets.ScreenMachine;
using Assets.Views;
using System;

namespace Assets.Controllers.States {
    public class PreMovementMenuStateController : BaseStateController<PreMovementMenuStateUiView, PreMovementMenuStateWorldView>, IStateBase {

        private const string Id = "PreMovementMenu";
        
        private readonly PreMovementMenuStateArgs stateArgs;

        public PreMovementMenuStateController(Context context, PreMovementMenuStateArgs stateArgs) : base(context) {
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
            uiView.OnCancel += PopState;
            uiView.OnMove += OnConfirmMovement;
        }

        private void OnAttack() {
			PopState();
		}

        public void OnDestroy() {
			uiView.OnAttack -= OnAttack;
			uiView.OnCancel -= PopState;
            uiView.OnMove -= OnConfirmMovement;
        }

        private void OnConfirmMovement() {
            stateArgs.OnMovementConfirmed?.Invoke();
            PopState();
        }

        public void OnSendToBack() {
            
        }

    }
}