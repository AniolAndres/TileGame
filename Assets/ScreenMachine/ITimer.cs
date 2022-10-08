using UnityEngine;
using System.Collections;
using System;

namespace Assets.ScreenMachine {
    public interface ITimer {

        void Fire();

        void AddCallback(Action callback);
    }
}