using Code.Global;
using UnityEngine;

namespace Code.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("UI Code")]
        public BoardUI BoardUI;
        public NationalPanelUI NationalPanelUI;
        
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

        private void Start()
        {
            EventManager.OnLogMessage.AddListener(UpdateLog);
            EventManager.OnTurnEnd.AddListener(OnTurnEnded);
        }

        /// <summary>
        /// EventManager.OnLogMessage 이벤트가 발생할 때 호출됩니다.
        /// </summary>
        /// <param name="message">표시할 로그 메시지</param>
        private void UpdateLog(string message)
        {
            Debug.Log($"<UI LOG> {message}");
        }

        /// <summary>
        /// EventManager.OnTurnEnd 이벤트가 발생할 때 호출됩니다.
        /// </summary>
        private void OnTurnEnded()
        {
            NationalPanelUI.gameObject.SetActive(true);
            Debug.Log("UI Manager: 턴 종료! 국가 패널 활성화.");
        }
    }
}