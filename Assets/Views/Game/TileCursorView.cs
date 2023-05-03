using System.Collections;
using UnityEngine;

namespace Assets.Views.Game {
	public class TileCursorView : MonoBehaviour {

		public void SetPosition(Vector2 position) {
			transform.AsRectTransform().localPosition = position;
		}

		public void SetActive(bool active) {
			gameObject.SetActive(active);
		}
	}
}