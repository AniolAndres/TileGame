

using Assets.Data;
using System.Collections.Generic;

namespace Assets.Controllers {
    public class GameStateArgs {

        public string LevelId { get; set; }

        public List<SetupArmyData> ArmyDatas { get; set; }
        
    }
}