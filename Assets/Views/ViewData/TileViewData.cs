using System.Collections;
using UnityEngine;

namespace Assets.Views {
	public struct TileViewData {

		public Color TileColor { get; set; }

		public float TileSideLength { get; set; }

		public float xPosition { get; set; } //Real world position
		public float yPosition { get; set; } //Real world Position

		public bool IsMainTile { get; set; }
	}
}