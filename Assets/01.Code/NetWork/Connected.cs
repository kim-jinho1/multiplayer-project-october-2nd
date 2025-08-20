using DG.Tweening;
using PurrNet;
using PurrNet.Transports;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.NetWork
{
    public class Connected : MonoBehaviour
    {
        [SerializeField] private NetworkManager  networkManager;
        [SerializeField] private TMP_Text connectedText;
        [SerializeField] private GameObject mainCamera;
        [SerializeField] private Image bg;
        
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
                Fade();
            }
            else if (obj == ConnectionState.Disconnected)
            {
                connectedText.text = "연결중...";
            }
        }

        private void Fade()
        {
            bg.DOFade(0, 5f).OnComplete(() =>
            {
                bg.gameObject.SetActive(false);
            });
            connectedText.DOFade(0, 5f).OnComplete(() =>
            {
                bg.gameObject.SetActive(false);
            });
        }
    }
}
