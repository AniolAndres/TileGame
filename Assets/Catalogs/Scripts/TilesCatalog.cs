using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Catalogs.Scripts {

    [CreateAssetMenu(fileName = "Tiles Catalog", menuName = "ScriptableObjects/Create Tiles Catalog", order = 2)]
    public class TilesCatalog : Catalog<TileCatalogEntry> {

    }
}