
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Extensions {
    public static class GenericExtensions {
        public static bool IsNullOrEmpty<T>(this IList<T> list) {
            return list == null || list.Count == 0;
        }

        public static bool IsEmpty<T>(this IList<T> list) {
            return list.Count == 0;
        }

        public static T GetElement<T>(this T[,] source, Vector2Int position) {
            return source[position.x, position.y];
        }

        public static IReadOnlyList<Vector2Int> GetNeighbours<T>(this T[,] source, Vector2Int position) {
            var width = source.GetLength(0);
            var height = source.GetLength(1); 

            var neighboursList = new List<Vector2Int>();
            if (position.x > 0) {
                neighboursList.Add(new Vector2Int(position.x-1, position.y));
            }

            if(position.x < width - 1) {
                neighboursList.Add(new Vector2Int(position.x + 1, position.y));
            }

            if (position.y > 0) {
                neighboursList.Add(new Vector2Int(position.x, position.y - 1));
            }

            if (position.y < height - 1) {
                neighboursList.Add(new Vector2Int(position.x, position.y + 1));
            }

            return neighboursList;
        }

    }
}