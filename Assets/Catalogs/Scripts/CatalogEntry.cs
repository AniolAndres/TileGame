using UnityEngine;
using System.Collections;

namespace Assets.Catalogs {
    public class CatalogEntry : ScriptableObject {

        [SerializeField]
        private string id;

        public string Id => id;

#if UNITY_EDITOR
		public void SetId(string id) {
			this.id = id;
		}
#endif
	}
}