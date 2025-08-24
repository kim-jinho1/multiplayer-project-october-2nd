using Code.Global;
using UnityEngine;

namespace Code.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("UI GameObject")]
        [SerializeField] private GameObject gameEndPanel;
        [SerializeField] private GameObject nationalPanel;
        [SerializeField] private GameObject technologyPanel;
        [SerializeField] private GameObject selectButtonPanel;
        [SerializeField] private GameObject battleResultPanel;
        [SerializeField] private GameObject unitInformationPanel;
        [SerializeField] private GameObject alarmPanel;
        [SerializeField] private GameObject unitPanel;
        [SerializeField] private GameObject nationalPanelOpenButton;
        [SerializeField] private GameObject eventPanel;

        public void UIOff(GameObject ui) => ui.SetActive(false);
        public void UIOn(GameObject ui) => ui.SetActive(true);
    }
}