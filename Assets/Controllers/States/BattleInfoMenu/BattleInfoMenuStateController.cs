
using Assets.Data;
using Assets.Data.Models;
using Assets.ScreenMachine;
using Assets.Views;

namespace Assets.Controllers {
    public class BattleInfoMenuStateController : BaseStateController<GameStateUiView, GameStateWorldView>, IStateBase {

        private const string Id = "BattleInfoMenu";

        private BattleInfoMenuModel model;

        private readonly BattleInfoMenuStateArgs stateArgs;

        public BattleInfoMenuStateController(Context context, BattleInfoMenuStateArgs stateArgs) : base(context) {
            this.stateArgs = stateArgs;
        }

        public string GetId() {
            return Id;
        }

        public void OnBringToFront() {

        }

        public void OnCreate() {

            model = new BattleInfoMenuModel();



        }


        public void OnDestroy() {

        }

        public void OnSendToBack() {

        }

    }

}