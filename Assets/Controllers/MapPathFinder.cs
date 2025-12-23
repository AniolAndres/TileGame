using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Catalogs;
using Assets.Data;
using Assets.Extensions;
using UnityEngine;

namespace Assets.Controllers {
    
 
    public class MapPathFinder
    {
        private List<Node> mappedNodes = new List<Node>(100);

        private readonly TileController[,] tileMap;

        private readonly MovementTypesCatalog movementTypeCatalog;
        
        private readonly UnitHandler unitHandler;

        private readonly Dictionary<string, Dictionary<string, int>> costDictionary = new Dictionary<string, Dictionary<string, int>>();

        public MapPathFinder(TileController[,] tileMap, UnitHandler unitHandler, MovementTypesCatalog movementTypesCatalog)
        {
            this.unitHandler = unitHandler;
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

        public List<Vector2Int> GetPath(Vector2Int destination) {

            var node = mappedNodes.FirstOrDefault(x => x.position == destination);
            if (node == null) {
                return null;
            }

            var path = new List<Vector2Int>();

            while(node.previousNode != null) {
                path.Add(node.position);
                node = node.previousNode;               
            }

            //this can happen if unit doesn't move
            if (path.IsEmpty())
            {
                path.Add(node.position);
            }

            path.Reverse();

            return path;
        }

        public List<Node> GetAvailableTiles(UnitCatalogEntry unitCatalogEntry, int currentArmyId, Vector2Int origin)
        {
            mappedNodes.Clear();

            var originNode = new Node { position = origin, accumulatedCost = 0 };
            
            mappedNodes.Add(originNode); //Origin is always available

            var movementTypeId = unitCatalogEntry.MovementTypeCatalogEntry.Id;
            var unitMaxMovement = unitCatalogEntry.UnitSpecificationConfig.Movemement;

            var nodesToCheck = new List<Node>();
            var nodesChecked = new HashSet<Vector2Int>();

            nodesToCheck.Add(originNode);

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

                    if (!costDictionary[movementTypeId].ContainsKey(type) || HasEnemyUnit(neighbourPosition)) {
                        continue;
                    }

                    var cost = costDictionary[movementTypeId][type] + currentExaminedNode.accumulatedCost;

                    var newNode = new Node { position = neighbourPosition, accumulatedCost = cost, previousNode = currentExaminedNode };

                    if (cost <= unitMaxMovement) {

                        var exists = GetElementWithPositionInAvailableNodes(neighbourPosition) != null;
                        if (!exists) {
                            mappedNodes.Add(newNode);
                        }
                    }

                    if (cost < unitMaxMovement) {

                        var element = GetElementWithPositionInNodesToCheck(neighbourPosition);

                        if(element != null && element.accumulatedCost > cost) {
                            element.accumulatedCost = cost;
                            element.previousNode = currentExaminedNode;
                        } else {
                            nodesToCheck.Add(newNode);
                        }
                    }
                }
            }

            return mappedNodes;

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
                foreach (var node in mappedNodes) {
                    if (position == node.position) {
                        return node;
                    }
                }

                return null;
            }
            
            bool HasEnemyUnit(Vector2Int position)
            {
                return !unitHandler.IsSpaceEmpty(position) && !unitHandler.IsFromArmy(position, currentArmyId);
            }
        }

        public List<Node> GetHighLightedTiles() {
            return mappedNodes;
        }
    }
}