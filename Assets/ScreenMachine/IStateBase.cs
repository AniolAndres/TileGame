using Assets.Views;

namespace Assets.ScreenMachine {
    public interface IStateBase {
        string GetId();

        void LinkViews(UiView uiView, WorldView worldView);

        void DestroyViews();

        void OnBringToFront();

        void OnSendToBack();

        void OnCreate();

        void OnDestroy();

        void OnUpdate();
    }

}