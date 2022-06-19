
using Assets.Views.States.Scripts;


namespace Assets.States {
    public class PopupState : BaseState<PopupStateUiView, PopupStateWorldView>, IStateBase {

        private const string Id = "PopupState";

        public PopupState(Context context) : base(context) { }

        public string GetId() {
            return Id;
        }

        public void OnBringToFront() {

        }

        public void OnCreate() {
            uiView.OnPopRequested += PopState;
        }

        public void OnDestroy() {
            uiView.OnPopRequested -= PopState;
        }

        public void OnSendToBack() {

        }

    }

}