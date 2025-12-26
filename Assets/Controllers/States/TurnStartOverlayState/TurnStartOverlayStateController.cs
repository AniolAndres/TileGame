using Assets.Controllers.States;
using Assets.Data;
using Assets.ScreenMachine;
using Assets.Views;

namespace Assets.Controllers
{
    //Overkill class, but I follow the pattern this way
    public class TurnStartOverlayStateController : BaseStateController<TurnStartOverlayUiView, DummyWorldView>, IStateBase
    {
        const string Id = "TurnStartOverlayState";
        
        readonly TurnStartOverlayStateArgs stateArgs;
        
        public TurnStartOverlayStateController(Context context, TurnStartOverlayStateArgs startOverlayStateArgs) : base(context)
        {
            stateArgs = startOverlayStateArgs;
        }

        public string GetId() {
            return Id;
        }

        public void OnCreate()
        {
            uiView.OnMovementCompleted += PopState;
            
            uiView.Setup(stateArgs.RoundNumber, stateArgs.PlayerName);
        }

        public void OnDestroy()
        {
            uiView.OnMovementCompleted -= PopState;
            
            stateArgs.OnStatePop?.Invoke(); //Ugly
        }

        public void OnBringToFront()
        {
        }

        public void OnSendToBack()
        {
        }
    }
}