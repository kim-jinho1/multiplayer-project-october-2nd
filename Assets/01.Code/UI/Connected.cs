using PurrNet;
using PurrNet.Transports;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class Connected : MonoBehaviour
    {
        [SerializeField] private NetworkManager  networkManager;
        [SerializeField] private TMP_Text connectedText;
        [SerializeField] private GameObject mainCamera;

        private void OnEnable()
        {
            networkManager.onClientConnectionState +=  OnConnectionState;
        }

        private void OnDestroy()
        {
            networkManager.onClientConnectionState -=  OnConnectionState;
        }

        private void OnConnectionState(ConnectionState obj)
        {
            if (obj == ConnectionState.Connected)
            {
                connectedText.text = "연결됨!";
                mainCamera.SetActive(false);
            }
            else if (obj == ConnectionState.Disconnected)
            {
                connectedText.text = "연결중...";
            }
        }
    }
}
