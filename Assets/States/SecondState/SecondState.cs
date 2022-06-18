using Assets.Views;
using UnityEngine;

namespace Assets.States {
    public class SecondState : BaseState, IStateBase {

        private SecondStateUiView uiView;

        private SecondStateWorldView worldView;

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

        private void PopState() {
            screenMachine.PopState();
        }

        public void OnDestroy() {
            uiView.OnPopRequested -= PopState;
        }

        public void OnSendToBack() {

        }

        public void LinkViews(UiView uiView, WorldView worldView) {
            this.uiView = uiView as SecondStateUiView;
            this.worldView = worldView as SecondStateWorldView;
        }

        public void DestroyViews() {
            Object.Destroy(uiView.gameObject);
            Object.Destroy(worldView.gameObject);
        }
    }

}