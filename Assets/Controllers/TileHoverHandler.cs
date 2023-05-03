
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Controllers {
	public class TileHoverHandler {

		private Vector2Int previousHoveredTile;

		readonly private InputCalculatorHelper inputCalculator;

		readonly private MapController mapController;

		public TileHoverHandler(InputCalculatorHelper inputCalculatorHelper, MapController mapController) {
			this.inputCalculator = inputCalculatorHelper;
			this.mapController = mapController;
		}


		public void OnHover() {
			var tile = inputCalculator.GetTileFromMousePosition();
			if (tile == previousHoveredTile) {
				return;
			}

			previousHoveredTile = tile;
			mapController.MoveTileCursorTo(tile);
		}
	}
}