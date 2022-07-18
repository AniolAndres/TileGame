using Assets.Views;
using UnityEngine;

namespace Assets.States {
    public class StartupState : BaseState<StartupStateUiView, StartupStateWorldView>, IStateBase {

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
            var gameStateArgs = new GameStateArgs {
                LevelId = "first"
            };
            PushState(new GameState(context, gameStateArgs));
        }

        private void PresentNewState() {
            var gameStateArgs = new GameStateArgs {
                LevelId = "first"
            };
            PresentState(new GameState(context, gameStateArgs));
        }

        public void OnDestroy() {
            uiView.OnPresentRequested -= PresentNewState;
            uiView.OnPushRequested -= PushNewState;
        }

        public void OnSendToBack() {

        }


    }

}