using Code.StrategicSystem;
using UnityEngine;

namespace Code.Global
{
    [CreateAssetMenu(fileName = "PolicyData", menuName = "SO/PolicyData", order = 0)]
    public class PolicyData : ScriptableObject
    {
        public string PolicyName;
        public string Description;
        public int GoldBonus;
        public int StabilityChange;
        public TechnologyLevel TechnologyToResearch; // TechBoostPolicy와 연결 가능
    }
}