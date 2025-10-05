

namespace Modules.AssetLoader {
    public interface IAssetLoaderFactory{

        IAssetLoader CreateLoader(string id); //Use for state Id, not asset Id
    }
}