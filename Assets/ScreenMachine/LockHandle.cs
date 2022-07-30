using UnityEngine;
using System.Collections;

namespace Assets.ScreenMachine {
    public class LockHandle {

        private InputLocker inputLocker;

        public LockHandle(InputLocker inputLocker) {
            this.inputLocker = inputLocker;
        }

        public void Unlock() {
            inputLocker.Unlock();
        }
    }
}