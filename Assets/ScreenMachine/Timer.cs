using System;
using System.Collections.Generic;

namespace Assets.ScreenMachine {
    public class Timer : ITimer {

        public List<Action> actionsOnComplete = new List<Action>();
        private float duration;
        private float internalTimer;

        public string StateId { get; }

        public event Action<Timer> OnComplete;

        private bool started;

        public Timer(float duration, string stateId) {
            this.duration = duration;
            StateId = stateId;
        }

        public void AddCallback(Action callback) {
            actionsOnComplete.Add(callback);
        }

        public void Fire() {
            started = true;
        }

        public TimerState OnUpdate(float timeDelta) {

            if (!started) {
                return TimerState.NotStarted;
            }

            internalTimer += timeDelta;
            
            if(internalTimer > duration) {
                foreach(var action in actionsOnComplete) {
                    action?.Invoke();
                }

                return TimerState.Completed;
            }

            return TimerState.Running;
        }
    }
}