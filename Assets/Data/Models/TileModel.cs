using UnityEngine;
using Assets.Data.Level;

namespace Assets.Data.Models {
    public class TileModel {

        private readonly TileType type;

        private readonly Vector2Int position;

        public Vector2Int Position => position;

        public TileModel(Vector2Int position) {
            this.position = position;
            type = Random.Range(0f, 1f) > 0.5f ? TileType.Grass : TileType.Water;
        }

    }
}