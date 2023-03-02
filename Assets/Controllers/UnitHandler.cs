
using Assets.Data;
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

        private MovementData lastMovementData; //could make this readonly, don't want to bother with checks inside the class for now

        public event Action OnMovementStop;

        public event Action OnMovementStart;

        public UnitHandler(GameplayInputLocker inputLocker) {
            this.inputLocker = inputLocker;
        }

        public bool HasUnitSelected => selectedUnitKey.HasValue;

        public void AddUnit(UnitController unitController, Vector2Int position) {
            if (unitControllerDictionary.ContainsKey(position)) {
                throw new NotSupportedException($"Trying to create {unitController.GetUnitId()} in ({position.x},{position.y})," +
                    $"but it's already occupied by {unitControllerDictionary[position]}");
            }

            unitController.OnMovementStart += FireMovementStartAction;
            unitController.OnMovementEnd += FireMovementEndAction;

            unitControllerDictionary[position] = unitController;
        }

        private void FireMovementEndAction() {
			if (inputLock == null) {
				throw new NotSupportedException("There should always be an active inputLock when ending a movement");
			}

			inputLock.Unlock();
            inputLock = null;
		}

        private void FireMovementStartAction() {
			if (inputLock != null) {
				throw new NotSupportedException("There should never be an active inputLock when starting a movement");
			}

			inputLock = inputLocker.LockInput();
		}

        public void RemoveUnitAtPosition(Vector2Int position) {
            if (!unitControllerDictionary.ContainsKey(position)) {
                throw new NotSupportedException($"Trying to remove unit in ({position.x},{position.y}), but there's none!");
            }

            var controller = unitControllerDictionary[position];
            controller.OnMovementEnd -= FireMovementEndAction;
            controller.OnMovementStart -= FireMovementStartAction;

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

        public void MoveSelectedUnit(List<Vector2Int> gridPositions, List<Vector2> realPositions) {

            if (selectedUnitKey == null) {
                throw new NotSupportedException("Moving selected unit without having anything selected!");
            }

            var previousPosition = selectedUnitKey.Value;

            var newPosition = gridPositions.Last();

            var empty = IsSpaceEmpty(newPosition);
            if (!empty) {
                return;
            }

            MoveUnitFromTo(selectedUnitKey.Value, newPosition);

            var controller = unitControllerDictionary[newPosition];
            controller.OnMove(previousPosition, gridPositions, realPositions);

            lastMovementData = new MovementData {
                Destination = newPosition,
                Origin = previousPosition,
            };

            selectedUnitKey = newPosition;
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

        public string GetSelectedUnitId()
        {
            if(selectedUnitKey == null) {
                throw new NotSupportedException("Trying to get id from selected unit without having anything selected!");
            }

            return unitControllerDictionary[selectedUnitKey.Value].GetUnitId();
        }

		public Vector2Int GetSelectedUnitPosition() {

            if (!selectedUnitKey.HasValue) {
                throw new NotSupportedException("Trying to get selected unit but there's none, check first");
            }

            return selectedUnitKey.Value;
		}

        public void UndoLastMove(Vector2 realOriginPosition) {
			if (!selectedUnitKey.HasValue) {
				throw new NotSupportedException("Trying to get selected while undoing the movement but there's none, should not ever happen");
			}

			MoveUnitFromTo(lastMovementData.Destination, lastMovementData.Origin); //Undo the move
            selectedUnitKey = lastMovementData.Origin;
            unitControllerDictionary[selectedUnitKey.Value].MoveToInstant(realOriginPosition);
        }

		public void ExhaustCurrentUnit() {
			if (!selectedUnitKey.HasValue) {
				throw new NotSupportedException("Trying to get selected unit but there's none, check first");
			}

            unitControllerDictionary[selectedUnitKey.Value].Exhaust() ;
		}

        public void CleanLastMove() {
            lastMovementData = null;
        }

        public MovementData GetLastMoveData() {
            return lastMovementData;
        }
    }
}