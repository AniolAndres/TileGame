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

            var assetLoaderFactory = new AssetLoaderFactory();

            screenMachine = new GameScreenMachine(catalogs.StatesCatalog, assetLoaderFactory);
            screenMachine.Init();

            var context = new Context {
                Catalogs = catalogs,
                UserData = new UserData(),
                AssetLoaderFactory = assetLoaderFactory,
                ScreenMachine = screenMachine
            };

            screenMachine.PushState(new StartupStateController(context));
        }

        private void Update() {
            screenMachine.OnUpdate();
        }

    }

}