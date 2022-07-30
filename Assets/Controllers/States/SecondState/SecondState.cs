using Assets.ScreenMachine;
using Assets.Views;

namespace Assets.Controllers {
    public class SecondState : BaseStateController<SecondStateUiView, SecondStateWorldView>, IStateBase {

        private const string Id = "SecondState";

        public SecondState(Context context) : base(context) { }

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