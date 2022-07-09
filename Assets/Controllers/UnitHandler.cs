
using Assets.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controllers {
    public class UnitHandler {

        private Dictionary<Vector2Int, UnitController> unitControllerDictionary = new Dictionary<Vector2Int, UnitController>();

        public void AddUnit(UnitController unitController, Vector2Int position) {
            if (unitControllerDictionary.ContainsKey(position)) {
                throw new NotSupportedException($"Trying to create {unitController.GetUnitId()} in ({position.x},{position.y})," +
                    $"but it's already occupied by {unitControllerDictionary[position]}");
            }

            unitControllerDictionary[position] = unitController;
        }

        public void RemoveUnitAtPosition(Vector2Int position) {
            if (!unitControllerDictionary.ContainsKey(position)) {
                throw new NotSupportedException($"Trying to remove unit in ({position.x},{position.y}), but there's none!");
            }

            unitControllerDictionary.Remove(position);
        }

        public bool IsSpaceEmpty(Vector2Int position) {
            return !unitControllerDictionary.ContainsKey(position);
        }

        public void MoveUnitFromTo(Vector2Int oldPos, Vector2Int newPos) {
            if (unitControllerDictionary.ContainsKey(newPos)) {
                throw new NotSupportedException($"Trying to move unit to ({newPos.x},{newPos.y}), but it's not empty!, check if it's empty first");
            }

            if (!unitControllerDictionary.ContainsKey(oldPos)) {
                throw new NotSupportedException($"Trying to move unit in ({oldPos.x},{oldPos.y}), but there's none!");
            }

            var controller = unitControllerDictionary[oldPos];
            unitControllerDictionary.Remove(oldPos);
            unitControllerDictionary[newPos] = controller;
        }

        public UnitController GetUnitControllerAtPosition(Vector2Int position) {
            if (!unitControllerDictionary.ContainsKey(position)) {
                throw new NotSupportedException($"Trying to move unit to ({position.x},{position.y}), but it's empty!");
            }

            return unitControllerDictionary[position];
        }
    }
}