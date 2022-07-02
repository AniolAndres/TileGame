using UnityEngine;

namespace Assets.Configs {

    [CreateAssetMenu(fileName = "UnitPurchaseConfig", menuName = "ScriptableObjects/Configs/Create Unit Purchase config", order = 1)]
    public class UnitPurchaseViewConfig : ScriptableObject {

        [SerializeField]
        private string nameKey;

        [SerializeField]
        private int cost;

        [SerializeField]
        private Sprite unitSprite;

        public string NameKey => nameKey;

        public int Cost => cost;

        public Sprite UnitSprite => unitSprite;
    }
}