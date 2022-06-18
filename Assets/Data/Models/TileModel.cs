using UnityEngine;
using Assets.Data.Level;

namespace Assets.Data.Models {
    public class TileModel {

        private readonly TileType type;

        public TileModel() {
            type = Random.Range(0f, 1f) > 0.5f ? TileType.Grass : TileType.Water;
        }
    }
}