using System;

namespace Modules.TimerFactory {
    public interface ITimer {

        void Fire();

        void AddCallback(Action callback);
    }
}