using Assets.Configs;

namespace Assets.Controllers
{
    public class UnitRemovedTrigger : ITriggerStrategy
    {
        readonly TriggerData _data;
        
        public UnitRemovedTrigger(TriggerData data)
        {
            _data = data;
        }
        
        //We should be passing some sort of unit data
        public bool IsTriggered(string triggerId)
        {
            return triggerId == _data.Data;
        }
    }
}