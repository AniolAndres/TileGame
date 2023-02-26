using UnityEngine;

namespace Assets.Configs {

    [CreateAssetMenu(fileName = "UnitSpecificationConfig", menuName = "ScriptableObjects/Configs/Create Unit Specification config", order = 1)]
    public class UnitSpecificationConfig : ScriptableObject {

        [SerializeField]
        private int cost;

        [SerializeField]
        private int movement;

        [SerializeField]
        private int vision;

        [SerializeField]
        private int attack;

        [SerializeField]
        private int minRange;

        [SerializeField]
        private int maxRange;

        public int Cost => cost;

        public int Movemement => movement;

        public int Vision => vision;

        public int Attack => attack;

        public int MinRange => minRange;

        public int MaxRange => maxRange;
    }
}
