
namespace Assets.States {
    public abstract class BaseState {

        protected Context context;

        protected IScreenMachine screenMachine => context.screenMachine;

        public BaseState(Context context) {
            this.context = context;
        }

        protected T GetStateAsset<T>() {
            return context.screenMachine.GetStateAsset<T>();
        }
    }
}