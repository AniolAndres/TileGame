using Assets.Catalogs.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.States {
    public abstract class BaseState {

        protected IScreenMachine screenMachine;

        public BaseState(IScreenMachine screenMachine) {
            this.screenMachine = screenMachine;
        }

        protected T GetStateAsset<T>() {
            return screenMachine.GetStateAsset<T>();
        }
    }
}