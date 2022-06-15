using Assets.Catalogs.Scripts;

using UnityEngine;

namespace Assets.States {
    public class MainComponent : MonoBehaviour {

        [SerializeField]
        private StatesCatalog statesCatalog;

        private IScreenMachine screenMachine;

        void Start() {
            screenMachine = new ScreenMachine();
            screenMachine.Init(statesCatalog);

            screenMachine.PushState(new StartupState(screenMachine));
        }


        private void Update() {

        }

    }

}