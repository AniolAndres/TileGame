using Assets.Data;
using Assets.ScreenMachine;
using Assets.Views.States.Scripts;

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
            uiView.OnPopRequested += PopState;
            uiView.OnConfirm += OnConfirmMovement;
        }

        public void OnDestroy() {
            uiView.OnPopRequested -= PopState;
            uiView.OnConfirm -= OnConfirmMovement;
        }

        private void OnConfirmMovement() {
            stateArgs.OnMovementConfirmed?.Invoke();
        }

        public void OnSendToBack() {
            
        }

    }
}