using UnityEngine;

namespace Assets.Views {
    public class TileMapView : MonoBehaviour {

        public TileView InstantiateTileView(TileView prefab, float x, float y) {
            var tileView = Instantiate(prefab,  this.transform);
            tileView.transform.AsRectTransform().anchoredPosition = new Vector2(x, y);
            return tileView;
        }
    }
}