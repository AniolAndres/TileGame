using Assets.Catalogs.Scripts;
using Assets.Data.Player;

namespace Assets.States {
    public class Context {

        public CatalogsHolder catalogs { get; set; }

        public UserData userData { get; set; }

        public IScreenMachine screenMachine { get; set; }

    }
}