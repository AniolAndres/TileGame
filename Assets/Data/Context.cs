
using Assets.Catalogs;
using Assets.ScreenMachine;
using Modules.AssetLoader;
using Modules.TimerFactory;

namespace Assets.Data {
    public class Context {

        public CatalogsHolder Catalogs { get; set; }

        public UserData UserData { get; set; }

        public IScreenMachine ScreenMachine { get; set; }

        public AssetLoaderFactory AssetLoaderFactory { get; set; }

        public TimerFactory TimerFactory { get; set; }

    }
}