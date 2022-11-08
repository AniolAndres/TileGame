using UnityEngine;
using System.Collections;

namespace Assets.Views {
    public struct UnitPurchaseViewData {

        public string Name { get; set; }

        public Sprite UnitIcon { get; set; }

        public string Cost { get; set; }

        public bool HasEnoughFunds { get; set; }

        public string Movement { get; set; }

        public string Attack { get; set; }

        public string Vision { get; set; }
    }
}