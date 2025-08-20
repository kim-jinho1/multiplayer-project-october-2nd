using Code.StrategicSystem;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class UnitUI : MonoSingleton<UnitUI>
    {
        [SerializeField] private GameObject unitPanel;
        [SerializeField] private TextMeshProUGUI unitInformationTitleText;
        
        public GameObject UnitPanel => unitPanel;
        public TextMeshProUGUI UnitInformationTitleText => unitInformationTitleText;
    }
}