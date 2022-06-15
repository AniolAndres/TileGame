using UnityEngine;
using System.Collections;
using System;

namespace Assets.Views {
    public class TileMapView : MonoBehaviour {

        [SerializeField]
        private TileView grassPrefab;

        [SerializeField]
        private TileView waterPrefab;

        public TileView InstantiateTileView(int i, int j) {
            Debug.Log($"Creating tile at {i} {j}");
            var position = transform.AsRectTransform().localPosition + new Vector3(i * 30f, j * 30f, 0f);
            return Instantiate(waterPrefab, position, Quaternion.identity, this.transform);
        }
    }
}