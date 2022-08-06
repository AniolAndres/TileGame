
using Assets.Catalogs;
using Assets.ScreenMachine;

namespace Assets.Data {
    public class Context {

        public CatalogsHolder Catalogs { get; set; }

        public UserData UserData { get; set; }

        public IScreenMachine ScreenMachine { get; set; }

        public AssetLoaderFactory AssetLoaderFactory { get; set; }

    }
}