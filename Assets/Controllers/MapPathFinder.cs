using System;
using System.Collections.Generic;
using Assets.Catalogs;
using Assets.Data;
using Assets.Extensions;
using UnityEngine;

namespace Assets.Controllers {
    
 
    public class MapPathFinder
    {
        private readonly TileController[,] tileMap;

        private readonly MovementTypesCatalog movementTypeCatalog; 

        private readonly Dictionary<string, Dictionary<string, int>> costDictionary = new Dictionary<string, Dictionary<string, int>>();

        public MapPathFinder(TileController[,] tileMap, MovementTypesCatalog movementTypesCatalog)
        {
            this.tileMap = tileMap;
            this.movementTypeCatalog = movementTypesCatalog;
        }

        public void Init() {

            //Build dictionary of dictionaries for fast access to data
            var entries = movementTypeCatalog.GetAllEntries();
            foreach (var entry in entries) {
                
                var currentDictionary = new Dictionary<string, int>(entry.MovementCostPairList.Count);
                costDictionary[entry.Id] = currentDictionary;

                foreach(var pair in entry.MovementCostPairList) {
                    currentDictionary[pair.TileEntry.Id] = pair.Cost;
                }
            }
        }

        public List<Node> GetAvailableTiles(UnitCatalogEntry unitCatalogEntry, Vector2Int origin)
        {
            var availableTiles = new List<Node>();

            var movementTypeId = unitCatalogEntry.MovementTypeCatalogEntry.Id;
            var unitMaxMovement = unitCatalogEntry.UnitSpecificationConfig.Movemement;

            var nodesToCheck = new List<Node>();
            var nodesChecked = new HashSet<Vector2Int>();

            nodesToCheck.Add(new Node {position = origin, accumulatedCost = 0 });

            while (nodesToCheck.Count != 0) {

                var currentExaminedNode = GetCheapestNode();
                nodesToCheck.Remove(currentExaminedNode);
                
                nodesChecked.Add(currentExaminedNode.position);

                var neighbourPositions = tileMap.GetNeighbours(currentExaminedNode.position);

                foreach (var neighbourPosition in neighbourPositions) {

                    if (nodesChecked.Contains(neighbourPosition)) {
                        continue;
                    }

                    var tileController = tileMap.GetElement(neighbourPosition);
                    var type = tileController.GetTileType();

                    if (!costDictionary[movementTypeId].ContainsKey(type)) {
                        continue;
                    }

                    var cost = costDictionary[movementTypeId][type] + currentExaminedNode.accumulatedCost;

                    var newNode = new Node { position = neighbourPosition, accumulatedCost = cost };

                    if (cost <= unitMaxMovement) {

                        var exists = GetElementWithPositionInAvailableNodes(neighbourPosition) != null;
                        if (!exists) {
                            availableTiles.Add(newNode);
                        }
                    }

                    if (cost < unitMaxMovement) {

                        var element = GetElementWithPositionInNodesToCheck(neighbourPosition);

                        if(element != null && element.accumulatedCost > cost) {
                            element.accumulatedCost = cost;
                        } else {
                            nodesToCheck.Add(newNode);
                        }
                    }
                }
            }

            return availableTiles;

            //------------------------------------------

            Node GetCheapestNode() {

                var smallestNode = nodesToCheck[0];

                for (int i = 1; i < nodesToCheck.Count; i++) {

                    var node = nodesToCheck[i];
                    if (smallestNode.accumulatedCost > node.accumulatedCost) {
                        smallestNode = node;
                    }
                }

                return smallestNode;
            }

            Node GetElementWithPositionInNodesToCheck(Vector2Int position) {
                foreach (var node in nodesToCheck) {
                    if(position == node.position) {
                        return node;
                    }
                }

                return null;
            }

            Node GetElementWithPositionInAvailableNodes(Vector2Int position) {
                foreach (var node in availableTiles) {
                    if (position == node.position) {
                        return node;
                    }
                }

                return null;
            }
        }

    }
}