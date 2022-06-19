using UnityEngine;

namespace Assets.Configs {

    [CreateAssetMenu(fileName = "CameraConfig", menuName = "ScriptableObjects/Configs/Create camera config", order = 1)]
    public class CameraConfig : StateAsset {
        [SerializeField]
        private float speed;

        [SerializeField]
        private float speedModifier;

        public float SpeedModifier => speedModifier;

        public float Speed => speed;
    }

}
