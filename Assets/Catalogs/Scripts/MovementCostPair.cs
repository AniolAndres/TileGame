using System;
using UnityEngine;

namespace Assets.Catalogs
{
    [Serializable]
    public class MovementCostPair
    {
        [SerializeField]
        private TileCatalogEntry tileEntry;

        [SerializeField]
        private int cost;

        public TileCatalogEntry TileEntry => tileEntry;

        public int Cost => cost;
    }
}