using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Configs;
using Codice.Client.BaseCommands;
using UnityEngine;

namespace Assets.Controllers
{
    public class GameTriggerHandler
    {
        private readonly DialogData[] dialogData;
        private readonly TriggerData[] triggerData;
        private readonly LevelEventDispatcher levelEventDispatcher;
        //Triggers here
        // more triggers
        // moooore

        public event Action<DialogData> OnPushDialogStateRequested;

        private readonly HashSet<string> completedDialogs =new HashSet<string>(24); //total arbitray number
        
        readonly GameTriggerFactory gameTriggerFactory = new GameTriggerFactory();

        private Dictionary<string, List<ITriggerStrategy>> triggerStrategies = new Dictionary<string, List<ITriggerStrategy>>();
        
        const string UnitKilledId = "UnitKilled";

        public GameTriggerHandler(LevelEventDispatcher levelEventDispatcher, DialogData[] dialogData, TriggerData[] triggerData)
        {
            this.dialogData = dialogData;
            this.levelEventDispatcher = levelEventDispatcher;
            this.triggerData =  triggerData;
        }

        public void Init()
        {
            InitializeTriggerDictionary();
            
            levelEventDispatcher.OnDialogRequested += HandleDialogRequested;
            levelEventDispatcher.OnUnitRemoved += HandleUnitRemoved;
        }

        private void InitializeTriggerDictionary()
        {
            foreach (var data in triggerData)
            {
                if (data.Kind == UnitKilledId)
                {
                    triggerStrategies[UnitKilledId] ??= new List<ITriggerStrategy>();
                    triggerStrategies[UnitKilledId].Add(new UnitRemovedTrigger(data));
                }
            }
        }

        public void Cleanup()
        {
            levelEventDispatcher.OnDialogRequested -= HandleDialogRequested;
            levelEventDispatcher.OnUnitRemoved -= HandleUnitRemoved;
        }

        private void HandleUnitRemoved(Vector2Int data)
        {
            var unitKilledTriggers = triggerStrategies[UnitKilledId];
            foreach (var trigger in unitKilledTriggers)
            {
                if (trigger.IsTriggered(data.ToString())) //PLEASE CHANGE THIS
                {
                    trigger.ACtion? lets continue here next ime
                    return;
                }
            }
        }

        private void HandleDialogRequested(string dialogID)
        {
            if (completedDialogs.Contains(dialogID))
            {
                return;
            }
            var dialog = dialogData.FirstOrDefault(x=> x.id == dialogID);
            OnPushDialogStateRequested?.Invoke(dialog);
            completedDialogs.Add(dialogID);
        }
        
        
    }
}