using Assets.Views;

namespace Assets.States {
    public interface IStateBase {
        string GetId();

        void LinkViews(UiView uiView, WorldView worldView);

        void DestroyViews();

        void OnBringToFront();

        void OnSendToBack();

        void OnCreate();

        void OnDestroy();
    }

}