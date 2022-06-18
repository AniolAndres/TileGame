using Assets.Catalogs.Scripts;
using Assets.Data.Player;

namespace Assets.States {
    public class Context {

        public CatalogsHolder catalogs { get; set; }

        public PlayerData playerData { get; set; }

        public IScreenMachine screenMachine { get; set; }

    }
}