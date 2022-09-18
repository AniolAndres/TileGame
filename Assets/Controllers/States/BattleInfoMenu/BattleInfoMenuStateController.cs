
using Assets.Data;
using Assets.Data.Models;
using Assets.ScreenMachine;
using Assets.Views;
using System;
using System.Threading.Tasks;

namespace Assets.Controllers {
    public class BattleInfoMenuStateController : BaseStateController<BattleInfoMenuStateUiView, BattleInfoMenuStateWorldView>, IStateBase {

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

            uiView.OnPopRequested += PopState;
            uiView.OnOptionClicked += OnOptionClicked;

            uiView.InstantiateOptions();

            model = new BattleInfoMenuModel();

        }


        public void OnDestroy() {
            uiView.OnOptionClicked -= OnOptionClicked;
            uiView.OnPopRequested -= PopState;
        }

        private void OnOptionClicked() {
            PopState();
            stateArgs.OnOptionClicked?.Invoke();
        }

        public void OnSendToBack() {

        }
    }

}