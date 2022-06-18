
using Assets.Data.Level;

namespace Assets.Levels {
    public interface ILevelProvider {

        LevelData GetLevel();
    }
}