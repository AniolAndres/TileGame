
namespace Assets.ScreenMachine {
    public class GameplayInputLocker {

        private readonly IScreenMachine screenMachine;

        public GameplayInputLocker(IScreenMachine screenMachine) {
            this.screenMachine = screenMachine;
        }

        public LockHandle LockInput() {
            return screenMachine.Lock();
        }
    }
}