using Code.StrategicSystem;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class UnitInformationUI : MonoSingleton<UnitInformationUI>
    {
        [SerializeField] private TextMeshProUGUI unitInformationTitleText;
        [SerializeField] private TextMeshProUGUI attackPowerText;
        [SerializeField] private TextMeshProUGUI defenseText;
        [SerializeField] private TextMeshProUGUI loyaltyText;
        
        public TextMeshProUGUI UnitInformationTitleText => unitInformationTitleText;
        public TextMeshProUGUI AttackPowerText => attackPowerText;
        public TextMeshProUGUI DefenseText => defenseText;
        public TextMeshProUGUI LoyaltyText => loyaltyText;
    }
}