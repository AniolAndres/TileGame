
using Assets.ScreenMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Controllers {
    public class UnitHandler {

        private readonly GameplayInputLocker inputLocker;

        private Dictionary<Vector2Int, UnitController> unitControllerDictionary = new Dictionary<Vector2Int, UnitController>();

        private Vector2Int? selectedUnitKey = null;

        private LockHandle inputLock;

        public UnitHandler(GameplayInputLocker inputLocker) {
            this.inputLocker = inputLocker;
        }

        public bool HasUnitSelected => selectedUnitKey.HasValue;

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

        public void SetUnitSelected(Vector2Int newSelectedPosition) {
            if (HasUnitSelected) {
                if (!unitControllerDictionary.ContainsKey(selectedUnitKey.Value)) {
                    throw new NotSupportedException($"Trying to deselect something that doesn't exist at ({selectedUnitKey.Value.x},{selectedUnitKey.Value.y})");
                }

                var controller = unitControllerDictionary[selectedUnitKey.Value];
                controller.OnDeselect();
            }

            if (!unitControllerDictionary.ContainsKey(newSelectedPosition)) {
                throw new NotSupportedException($"Trying to select something that doesn't exist at ({newSelectedPosition.x},{newSelectedPosition.y})");
            }

            unitControllerDictionary[newSelectedPosition].OnSelect();
            
            selectedUnitKey = newSelectedPosition;
        }

        public void MoveSelectedUnit(Vector2Int newPosition, Vector2 realNewPosition) {
            if(selectedUnitKey == null) {
                throw new NotSupportedException("Moving selected unit without having anything selected!");
            }

            var empty = IsSpaceEmpty(newPosition);
            if (!empty) {
                return;
            }

            MoveUnitFromTo(selectedUnitKey.Value, newPosition);

            inputLock = inputLocker.LockInput();

            var controller = unitControllerDictionary[newPosition];
            controller.OnMove(realNewPosition);
            controller.OnDeselect();

            selectedUnitKey = null;
        }

        public void TryUnlockInput() {
            if(inputLock == null) {
                throw new NotSupportedException("You're tyring to unlock input after movement, but it was never locked in the first place");
            }

            inputLock?.Unlock();
        }

        public bool IsFromArmy(Vector2Int position, int currentArmyIndex)
        {
            if (!unitControllerDictionary.ContainsKey(position)) {
                throw new NotSupportedException($"Trying to check if unit in ({position.x},{position.y}) belongs to army {currentArmyIndex} but it doesn't exist!");
            }

            var unitController = unitControllerDictionary[position];
            return unitController.GetUnitArmyId() == currentArmyIndex;
        }

        public void DeselectSelectedUnit()
        {
            if (selectedUnitKey.HasValue)
            {
                unitControllerDictionary[selectedUnitKey.Value].OnDeselect();
            }
            selectedUnitKey = null;
        }

        public void RefreshAllUnitsFromArmy(int armyIndex)
        {
            var units = unitControllerDictionary.Where(x => IsFromArmy(x.Key, armyIndex)).ToList();
            foreach (var unit in units)
            {
                unit.Value.Refresh();
            }
        }

        public bool CanUnitMove(Vector2Int position)
        {
            if (!unitControllerDictionary.ContainsKey(position))
            {
                throw new NotSupportedException($"Trying to check if a unit in ({position.x},{position.y}) can move but unit does not exist!");
            }

            return unitControllerDictionary[position].CanMove();
        }
    }
}