using Assets.Data;
using Assets.ScreenMachine;
using Assets.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Controllers {
    public class StartupStateController : BaseStateController<StartupStateUiView, StartupStateWorldView>, IStateBase, IPreloadable {

        private const string Id = "StartUpState";

        public StartupStateController(Context context) : base(context) { }

        private IAssetLoader assetLoader;

        public string GetId() {
            return Id;
        }

        public void OnBringToFront() {

        }

        public void OnCreate() {

            uiView.OnPresentRequested += PresentNewState;
            uiView.OnPushRequested += PushNewState;
        }

        private void PushNewState() {
            var gameStateArgs = new GameStateArgs {
                LevelId = "first",
                CommanderIds = GetCommanderIds()
            };
            PushState(new GameStateController(context, gameStateArgs));
        }

        private List<string> GetCommanderIds() {
            return new List<string> { "andy", "colin" };
        }

        private void PresentNewState() {
            var gameStateArgs = new GameStateArgs {
                LevelId = "first",
                CommanderIds = GetCommanderIds()
            };
            PresentState(new GameStateController(context, gameStateArgs));
        }

        public void OnDestroy() {
            uiView.OnPresentRequested -= PresentNewState;
            uiView.OnPushRequested -= PushNewState;
        }

        public void OnSendToBack() {
            
        }

        public Task Preload() {

            assetLoader = assetLoaderFactory.CreateLoader(this);

            Debug.Log("preloading state");

            return assetLoader.LoadAsync();
        }
    }

}