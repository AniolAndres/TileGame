using System.Collections.Generic;
using Assets.Catalogs;
using UnityEngine;

namespace Assets.Controllers
{
    public class MapPathFinder
    {
        private readonly TileController[,] tileMap;

        public MapPathFinder(TileController[,] tileMap)
        {
            this.tileMap = tileMap;
        }
        
        

        public List<Vector2Int> GetAvailableTiles(UnitCatalogEntry unitCatalogEntry, Vector2Int origin)
        {
            var movementDictionary = new Dictionary<string, int>(unitCatalogEntry.MovementTypeCatalogEntry.MovementCostPair.Count);
            movementDictionary.W
            var movement = unitCatalogEntry.UnitSpecificationConfig.Movemement;
            var movementTypeEntry = unitCatalogEntry.MovementTypeCatalogEntry;
            var type = tileMap[origin.x, origin.y].GetTileType();
        }
    }
}