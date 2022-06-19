using UnityEngine;
using System.Collections;

namespace Assets.Views {
    public static class ViewExtensions {

        public static RectTransform AsRectTransform(this Transform transform) {
            return transform as RectTransform;
        }

    }
}