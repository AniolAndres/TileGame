using UnityEngine;

namespace Assets.Views {
    public class TileMapView : MonoBehaviour {

        [SerializeField]
        private TileView grassPrefab;

        [SerializeField]
        private TileView waterPrefab;

        public TileView InstantiateTileView(float x, float y) {
            var tileView = Instantiate(waterPrefab,  this.transform);
            tileView.transform.AsRectTransform().anchoredPosition = new Vector2(x, y);
            return tileView;
        }

        public void CenterCamera(float width, float height) {
            transform.AsRectTransform().localPosition = new Vector2(0f, 0f);
        }
    }
}