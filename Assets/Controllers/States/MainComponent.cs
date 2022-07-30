using Assets.Catalogs.Scripts;
using Assets.Data.Player;
using Assets.ScreenMachine;
using UnityEngine;

namespace Assets.Controllers {
    public class MainComponent : MonoBehaviour {

        [SerializeField]
        private CatalogsHolder catalogs;

        private GameScreenMachine screenMachine;

        void Start() {

            screenMachine = new GameScreenMachine();
            screenMachine.Init(catalogs.StatesCatalog);

            var context = new Context {
                catalogs = catalogs,
                userData = new UserData(),
                screenMachine = screenMachine
            };

            screenMachine.PushState(new StartupStateController(context));
        }

        private void Update() {
            screenMachine.OnUpdate();
        }

    }

}