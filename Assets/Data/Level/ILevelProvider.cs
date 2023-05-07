
using Assets.Configs;
using Assets.Data.Level;

namespace Assets.Data.Levels {
    public interface ILevelProvider {
        SerializableLevelData GetLevel(string id);
    }
}