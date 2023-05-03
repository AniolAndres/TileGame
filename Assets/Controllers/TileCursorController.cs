using Assets.Views;
using Assets.Views.Game;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Controllers {
	public class TileCursorController {

		readonly TileCursorView cursorView;

		public TileCursorController(TileCursorView cursorView) {
			this.cursorView = cursorView;	
		}

		public void SetPosition(Vector2 realPosition) {
			cursorView.SetPosition(realPosition);
		}
	}
}