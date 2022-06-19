using UnityEngine;

namespace Assets.Configs {

    [CreateAssetMenu(fileName = "CameraConfig", menuName = "ScriptableObjects/Configs/Create camera config", order = 1)]
    public class CameraConfig : StateAsset {
        [SerializeField]
        private float speed;

        [SerializeField]
        private Vector2Int tileSize;

        public Vector2Int TileSize => tileSize;

        public float Speed => speed;
    }

}
