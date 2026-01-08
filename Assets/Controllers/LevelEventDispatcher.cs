using System;
using UnityEngine;

namespace Assets.Controllers
{
    public class LevelEventDispatcher
    {
        public event Action<Vector2Int> OnUnitAdded; //Position
        public event Action<Vector2Int> OnUnitRemoved; //Position
        public event Action<int> OnRoundStart; //Number of the turn that is starting, 0-based
        public event Action<int, PlayerController> OnTurnStart; //Number of the turn that is starting, 0-based

        public delegate void OnDialogRequestedDelegate(string dialogID);
        public event OnDialogRequestedDelegate OnDialogRequested; //Playing around with delegates, it could be an Action

        private readonly WarController warController;
        private readonly UnitHandler unitHandler;

        public LevelEventDispatcher(WarController warController, UnitHandler unitHandler)
        {
            this.warController = warController;
            this.unitHandler = unitHandler;
        }
        
        public void Init()
        {
            
            unitHandler.OnUnitRemoved += FireUnitRemovedEvent;
            unitHandler.OnUnitCreated += FireUnitCreatedEvent;

            warController.OnTurnStart += FireTurnStartEvent;
            warController.OnRoundStart += FireRoundStartEvent;
        }

        public void OnTearDown()
        {
            unitHandler.OnUnitRemoved -= FireUnitRemovedEvent;
            unitHandler.OnUnitCreated -= FireUnitCreatedEvent;

            warController.OnTurnStart -= FireTurnStartEvent;
            warController.OnRoundStart -= FireRoundStartEvent;
        }

        private void FireRoundStartEvent(int roundNumber)
        {
            OnRoundStart?.Invoke(roundNumber);
        }

        private void FireTurnStartEvent(int turnNumber, PlayerController playerController)
        {
            OnTurnStart?.Invoke(turnNumber, playerController);
        }

        private void FireUnitCreatedEvent(Vector2Int position)
        {
            OnUnitAdded?.Invoke(position);
        }

        private void FireUnitRemovedEvent(Vector2Int position)
        {
            OnUnitRemoved?.Invoke(position);
        }
    }
}