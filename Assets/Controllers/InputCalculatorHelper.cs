

using Assets.Extensions;
using UnityEngine;

namespace Assets.Controllers {
	public class InputCalculatorHelper {

		readonly MapController mapController;

		float tileSideSize;

		Vector2Int mapSize;

		Vector2 mapCenter;

		public InputCalculatorHelper(MapController mapController) {
			this.mapController = mapController;	
		}

		public void Init() {
			tileSideSize = mapController.GetTileSize();
			mapSize = mapController.GetMapSize();
		}


		public Vector2Int GetTileFromMousePosition() {
			var mapPosition = mapController.GetMapCenterCurrentPosition();
			var realMapPosition = new Vector2(-mapPosition.x, -mapPosition.y); //Camera doesn't move, is that map that does so I need to negate
			var screenSize = ScreenHelper.GetScreenSize();
			var mapTotalSize = mapSize.ToVector2() * tileSideSize;
			var halfMapSize = mapTotalSize / 2f;
			var mousePosition = Input.mousePosition.ToVector2();
			var halfScreenSize = screenSize / 2f;
			var cursorToCamera = mousePosition - halfScreenSize;
			var realCursorPosition = (halfMapSize + realMapPosition + cursorToCamera)/tileSideSize;
			var flooredPosition = new Vector2Int(Mathf.FloorToInt(realCursorPosition.x), Mathf.FloorToInt(realCursorPosition.y));

			return flooredPosition;
		}
	
	}
}