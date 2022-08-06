using Assets.Data;
using Assets.ScreenMachine;
using Assets.Views;

namespace Assets.Controllers {
    public class StartupStateController : BaseStateController<StartupStateUiView, StartupStateWorldView>, IStateBase {

        private const string Id = "StartUpState";

        public StartupStateController(Context context) : base(context) { }

        public string GetId() {
            return Id;
        }

        public void OnBringToFront() {

        }

        public void OnCreate() {
            uiView.OnPresentRequested += PresentNewState;
            uiView.OnPushRequested += PushNewState;
        }

        private void PushNewState() {
            var gameStateArgs = new GameStateArgs {
                LevelId = "first"
            };
            PushState(new GameStateController(context, gameStateArgs));
        }

        private void PresentNewState() {
            var gameStateArgs = new GameStateArgs {
                LevelId = "first"
            };
            PresentState(new GameStateController(context, gameStateArgs));
        }

        public void OnDestroy() {
            uiView.OnPresentRequested -= PresentNewState;
            uiView.OnPushRequested -= PushNewState;
        }

        public void OnSendToBack() {

        }


    }

}