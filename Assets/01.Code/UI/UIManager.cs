using Code.Global;
using UnityEngine;

namespace Code.UI
{
    public class UIManager : MonoBehaviour
    {
        public BoardUI BoardUI;
        public NationalPanelUI NationalPanelUI;

        private void Start()
        {
            EventManager.OnLogMessage.AddListener(UpdateLog);
            EventManager.OnTurnEnd.AddListener(OnTurnEnded);
        }

        private void UpdateLog(string message)
        {
            Debug.Log($"<UI LOG> {message}");
        }

        private void OnTurnEnded()
        {
            NationalPanelUI.gameObject.SetActive(true);
        }
    }
}