using System;
using _01.Code.StrategicSystem;
using Code.StrategicSystem;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class UnitUI : MonoSingleton<UnitUI>
    {
        [SerializeField] private GameObject unitPanel;
        [SerializeField] private TextMeshProUGUI unitInformationTitleText;
        public Action OnSelectBoard; 
        
        public GameObject UnitPanel => unitPanel;
        public TextMeshProUGUI UnitInformationTitleText => unitInformationTitleText;

        public void Action()
        {
            OnSelectBoard?.Invoke();
        }
    }
}