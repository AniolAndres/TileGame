using UnityEngine;
using System.Collections;
using System;

namespace Assets.Data.Models {
    public class TileMapModel {

        private readonly Vector2Int size;

        public TileMapModel(Vector2Int size) {
            this.size = size;
        }

        public Vector2Int GetSize() {
            return size;
        }
    }
}