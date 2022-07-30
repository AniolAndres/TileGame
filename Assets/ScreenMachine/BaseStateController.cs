using UnityEngine;
using Assets.Views;
using System;

namespace Assets.ScreenMachine {
    public abstract class BaseStateController<TuiView, TWorldView> 
        where TuiView : UiView 
        where TWorldView : WorldView {

        protected TuiView uiView;

        protected TWorldView worldView;

        protected Context context;

        private IScreenMachine screenMachine => context.screenMachine;

        public BaseStateController(Context context) {
            this.context = context;
        }

        protected T GetStateAsset<T>() {
            return context.screenMachine.GetStateAsset<T>();
        }

        protected void PopState() {
            screenMachine.PopState();
        }

        protected void PushState(IStateBase state) {
            screenMachine.PushState(state);
        }

        protected void PresentState(IStateBase state) {
            screenMachine.PresentState(state);
        }

        public void OnUpdate() {
            uiView.OnUpdate();
            worldView.OnUpdate();
        }

        public void DisableRaycasts() {
            uiView.DisableRaycast();
            worldView.DisableRaycast();
        }

        public void EnableRaycasts() {
            uiView.EnableRaycast();
            worldView.EnableRaycast();
        }

        public void LinkViews(UiView uiView, WorldView worldView) {
            this.uiView = uiView as TuiView;
            this.worldView = worldView as TWorldView;
        }

        public void DestroyViews() {
            GameObject.Destroy(uiView.gameObject);
            GameObject.Destroy(worldView.gameObject);
        }
    }
}