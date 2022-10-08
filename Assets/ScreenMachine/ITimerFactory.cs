
namespace Assets.ScreenMachine {
    public interface ITimerFactory {

        ITimer CreateTimer(IStateBase state, float duration);

    }
}