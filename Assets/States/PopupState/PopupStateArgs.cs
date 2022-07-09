using UnityEngine;
using System;
using Assets.Data;

namespace Assets.States {
    public class PopupStateArgs {

        public Action<BuyUnitData> OnUnitCreated;

        public Vector2Int Position { get; set; }
    }
}