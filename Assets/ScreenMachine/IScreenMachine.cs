

namespace Assets.ScreenMachine {
    public interface IScreenMachine {
        void Init();

        void PresentState(IStateBase state);

        void PushState(IStateBase state);

        void PopState();

        LockHandle Lock();
    }

}