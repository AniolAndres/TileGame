using UnityEngine;
using System.Collections;
using System;
using Assets.Data.Level;

namespace Assets.Controllers {
    public interface ITileController {

        void OnCreate();

        void OnDestroy();

        event Action<TileData> OnTileClicked;

    }
}