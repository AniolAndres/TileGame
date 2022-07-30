using Assets.Catalogs.Scripts;

namespace Assets.ScreenMachine {
    public interface IScreenMachine {
        void Init(StatesCatalog statesCatalog);

        void PresentState(IStateBase state);

        void PushState(IStateBase state);

        void PopState();

        LockHandle Lock();

        T GetStateAsset<T>();
    }

}