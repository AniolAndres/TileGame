using Assets.Catalogs.Scripts;

namespace Assets.States {
    public interface IScreenMachine {
        void Init(StatesCatalog statesCatalog);

        void PresentState(IStateBase state);

        void PushState(IStateBase state);

        void PopState();

        T GetStateAsset<T>();
    }

}