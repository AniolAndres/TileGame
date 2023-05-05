using Assets.Catalogs;
using System.Collections;
using UnityEngine;

namespace Assets.Controllers {
	public struct BattleConfiguration {

		public UnitCatalogEntry AttackerUnit { get; set; }
		public UnitCatalogEntry DefenderUnit { get; set; }

		public Vector2Int AttackerPosition { get; set; }
		public Vector2Int DefenderPosition { get; set; }

		public TileCatalogEntry AttackerTile { get; set; }
		public TileCatalogEntry DefenderTile { get; set; }

		public int AttackerHp { get; set; }

		public int DefenderHp { get; set; }
	}
}