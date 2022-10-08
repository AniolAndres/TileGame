using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Assets.ScreenMachine {
    public class TimerFactory : ITimerFactory {

        private Dictionary<string, List<Timer>> timersDictionary = new Dictionary<string, List<Timer>>();

        private List<Timer> finishedTimers = new List<Timer>(4); // I think 4 is enough, it's the default anyway

        public ITimer CreateTimer(IStateBase state, float duration) {

            var stateId = state.GetId();
            if (!timersDictionary.ContainsKey(stateId)) {
                timersDictionary[stateId] = new List<Timer>();
            }

            var timer = new Timer(duration, stateId);
            timer.OnComplete += DestroyTimer;
            timersDictionary[stateId].Add(timer);

            return timer;
        }

        private void DestroyTimer(Timer timerToDestroy) {
            if (!timersDictionary.ContainsKey(timerToDestroy.StateId)) {
                throw new NotSupportedException($"Trying to destroy a timer belonging to state {timerToDestroy.StateId}, but dictionary doesn't contain that state!");
            }

            timersDictionary[timerToDestroy.StateId].Remove(timerToDestroy);
        }

        public void DestroyAllTimersFromState(IStateBase stateBase) {
            var stateId = stateBase.GetId();

            if (!timersDictionary.ContainsKey(stateId)) {
                return; //It can be that a state hasnt created any timers at all
            }

            timersDictionary.Remove(stateId); //This should remove all timers through Garbage collector right? 98.6% sure
            //Maybe force complete them? have to think about it
        }

        public void UpdateTimers(float deltaTime) {
            foreach(var stateTimers in timersDictionary) {
                var timerList = stateTimers.Value;

                foreach(var timer in timerList) {
                    var state = timer.OnUpdate(deltaTime);

                    if(state == TimerState.Completed) {
                        finishedTimers.Add(timer);
                    }
                }

                if(finishedTimers.Count == 0) {
                    continue;
                }

                foreach(var finishedTimer in finishedTimers) {
                    timerList.Remove(finishedTimer);
                }

                finishedTimers.Clear();
            }
        }
    }
}