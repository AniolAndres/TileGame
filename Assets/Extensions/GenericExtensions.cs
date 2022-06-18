
using System.Collections.Generic;

namespace Assets.Extensions {
    public static class GenericExtensions {
        public static bool IsNullOrEmpty<T>(this IList<T> list) {
            return list == null || list.Count == 0;
        }

    }
}