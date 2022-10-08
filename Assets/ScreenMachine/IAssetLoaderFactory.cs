

namespace Assets.ScreenMachine {
    public interface IAssetLoaderFactory{

        IAssetLoader CreateLoader(IStateBase state);
    }
}