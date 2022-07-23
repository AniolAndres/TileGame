using Assets.Catalogs.Scripts;
using Assets.Data.Player;
using UnityEngine;

namespace Assets.States {
    public class MainComponent : MonoBehaviour {

        [SerializeField]
        private CatalogsHolder catalogs;

        void Start() {

            var screenMachine = new ScreenMachine();
            screenMachine.Init(catalogs.StatesCatalog);

            var context = new Context {
                catalogs = catalogs,
                userData = new UserData(),
                screenMachine = screenMachine
            };

            screenMachine.PushState(new StartupStateController(context));
        }

    }

}