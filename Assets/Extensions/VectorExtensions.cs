using UnityEngine;
using System.Collections;

namespace Assets.Extensions {
    public static class VectorExtensions {

        public static Vector3 ToVector3(this Vector2Int v2) {
            return new Vector3(v2.x, v2.y, 0.0f);
        }

        public static Vector2 ToVector2(this Vector2Int v2) {
            return new Vector2(v2.x, v2.y);
        }
    }
}