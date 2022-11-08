using UnityEngine;
using System;
using Assets.Data;

namespace Assets.Controllers {
    public class CreateUnitStateArgs {

        public Action<BuyUnitData> OnUnitCreated;

        public int CurrentFunds { get; set; }

        public string TileTypeId { get; set; }

        public Vector2Int Position { get; set; }
    }
}