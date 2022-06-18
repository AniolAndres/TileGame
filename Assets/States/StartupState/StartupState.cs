using Assets.Views;
using UnityEngine;

namespace Assets.States {
    public class StartupState : BaseState, IStateBase {

        private StartupStateUiView uiView;

        private StartupStateWorldView worldView;

        private const string Id = "StartUpState";

        public StartupState(Context context) : base(context) { }

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
            screenMachine.PushState(new GameState(context));
        }

        private void PresentNewState() {
            screenMachine.PresentState(new GameState(context));
        }

        public void OnDestroy() {
            uiView.OnPresentRequested -= PresentNewState;
            uiView.OnPushRequested -= PushNewState;
        }

        public void OnSendToBack() {

        }

        public void LinkViews(UiView uiView, WorldView worldView) {
            this.uiView = uiView as StartupStateUiView;
            this.worldView = worldView as StartupStateWorldView;
        }

        public void DestroyViews() {
            Object.Destroy(uiView.gameObject);
            Object.Destroy(worldView.gameObject);
        }
    }

}