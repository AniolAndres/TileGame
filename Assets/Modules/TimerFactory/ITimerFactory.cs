
namespace Modules.TimerFactory {
    public interface ITimerFactory {

        ITimer CreateTimer(string id, float duration);

        void DestroyAllTimersWithId(string id);
    }
}