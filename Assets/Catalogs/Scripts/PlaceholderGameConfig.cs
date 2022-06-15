using UnityEngine;
using System.Collections;

namespace Assets.Catalogs.Scripts {
    [CreateAssetMenu(fileName = "State configs", menuName = "ScriptableObjects/Create PH gameState config", order = 1)]
    public class PlaceholderGameConfig : StateAsset {

        [SerializeField]
        private string value;

        public string Value => value;

    }
}