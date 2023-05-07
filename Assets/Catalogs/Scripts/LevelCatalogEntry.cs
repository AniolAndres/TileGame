using UnityEngine;
using Assets.Configs;
using System.Collections;
using Unity.Plastic.Newtonsoft.Json;

namespace Assets.Catalogs {

    [CreateAssetMenu(fileName = "LevelCatalogEntry", menuName = "ScriptableObjects/Catalogs/Create level Catalog Entry", order = 1)]
    public class LevelCatalogEntry : CatalogEntry {

        [SerializeField]
        private int playersCount;

        [SerializeField]
        private Vector2Int size;

        [SerializeField]
        private TextAsset levelJson;

        [SerializeField]
        private float tileSideLength;

        public TextAsset LevelJson => levelJson;

        public Vector2Int Size => size;

        public float TileSideLength => tileSideLength;

        public int PlayersCount => playersCount;
    }
}