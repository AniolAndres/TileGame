using UnityEngine;
using System.Collections;

namespace Assets.Catalogs.Scripts {
    public class CatalogEntry : ScriptableObject {

        [SerializeField]
        private string id;

        public string Id => id;

    }
}