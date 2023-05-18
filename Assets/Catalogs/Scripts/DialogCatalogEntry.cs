using System.Collections;
using UnityEngine;

namespace Assets.Catalogs {

	[CreateAssetMenu(fileName = "DialogCatalogEntry", menuName = "ScriptableObjects/Catalogs/Create dialog Catalog Entry", order = 1)]
	public class DialogCatalogEntry : CatalogEntry {

		[SerializeField]
		private TextAsset dialogJson;

		public TextAsset DialogJson => dialogJson;
	}
}