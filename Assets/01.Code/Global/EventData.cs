using UnityEngine;

namespace Code.Global
{
    [CreateAssetMenu(fileName = "EventData", menuName = "SO/EventData", order = 0)]
    public class EventData : ScriptableObject
    {
        public string EventTitle;
        public string EventDescription;
        public int GoldImpact;
    }
}