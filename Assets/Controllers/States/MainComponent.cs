using Assets.Catalogs;
using Assets.Data;
using Assets.ScreenMachine;
using UnityEngine;

namespace Assets.Controllers {
    public class MainComponent : MonoBehaviour {

        [SerializeField]
        private CatalogsHolder catalogs;

        private GameScreenMachine screenMachine;

        void Start() {

            screenMachine = new GameScreenMachine(catalogs.StatesCatalog);
            screenMachine.Init();

            var context = new Context {
                Catalogs = catalogs,
                UserData = new UserData(),
                AssetLoaderFactory = new AssetLoaderFactory(),
                ScreenMachine = screenMachine
            };

            screenMachine.PushState(new StartupStateController(context));
        }

        private void Update() {
            screenMachine.OnUpdate();
        }

    }

}