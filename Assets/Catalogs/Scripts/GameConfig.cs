using UnityEngine;
using System.Collections;

namespace Assets.Catalogs.Scripts {

    [CreateAssetMenu(fileName = "State configs", menuName = "ScriptableObjects/Create gameState config", order = 1)]
    public class GameConfig : StateAsset {

        [SerializeField]
        private int value;

        public int Value => value;
         
    }
}