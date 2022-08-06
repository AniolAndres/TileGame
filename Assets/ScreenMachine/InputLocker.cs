
using System;

namespace Assets.ScreenMachine {
    public class InputLocker {

        public bool IsInputLocked { private set; get; }

        private IStateBase state;

        public void Lock(IStateBase currentState) {
            IsInputLocked = true;
            state = currentState;
            currentState.DisableRaycasts();
        }

        public void Unlock() {
            if(state == null) {
                throw new NotSupportedException("State is null! maybe it was destroyed while still locked?");
            }

            IsInputLocked = false;
            state.EnableRaycasts();
        }
    }
}