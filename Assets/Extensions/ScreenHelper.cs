using System.Collections;
using UnityEngine;

namespace Assets.Extensions {
	public static class ScreenHelper {

		public static Vector2 GetScreenSize() {
			return new Vector2(Screen.width, Screen.height);
		}
		
	}
}