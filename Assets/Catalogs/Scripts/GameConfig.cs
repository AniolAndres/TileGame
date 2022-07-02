using UnityEngine;
using Assets.Configs;

namespace Assets.Catalogs {

    [CreateAssetMenu(fileName = "State configs", menuName = "ScriptableObjects/Configs/Create gameState config", order = 1)]
    public class GameConfig : StateAsset {

        [SerializeField]
        private int value;

        public int Value => value;
         
    }
}