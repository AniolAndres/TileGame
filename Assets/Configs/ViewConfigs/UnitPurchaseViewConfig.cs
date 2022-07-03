using UnityEngine;

namespace Assets.Configs {

    [CreateAssetMenu(fileName = "UnitPurchaseConfig", menuName = "ScriptableObjects/Configs/Create Unit Purchase config", order = 1)]
    public class UnitPurchaseViewConfig : ScriptableObject {

        [SerializeField]
        private string nameKey;

        [SerializeField]
        private Sprite unitSprite;

        [SerializeField]
        private Sprite fullBodySprite;

        public string NameKey => nameKey;

        public Sprite UnitSprite => unitSprite;

        public Sprite FullBodySprite => fullBodySprite;
    }
}