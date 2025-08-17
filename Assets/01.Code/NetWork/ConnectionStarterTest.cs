using System.Collections;
using PurrNet;
using PurrNet.Logging;
using PurrNet.Transports;
using UnityEngine;

#if UTP_LOBBYRELAY
using PurrNet.UTP;
using Unity.Services.Relay.Models;
#endif

namespace PurrLobby
{
    public class ConnectionStarterTest : MonoBehaviour
    {
        private NetworkManager _networkManager;

        // LobbyDataHolder 대신 직접 값 설정
        [SerializeField] private string roomName = "MyTestRoom123";
        [SerializeField] private bool isHost = true;

        private void Awake()
        {
            if (!TryGetComponent(out _networkManager))
            {
                PurrLogger.LogError($"Failed to get {nameof(NetworkManager)} component.", this);
            }
        }

        private void Start()
        {
            if (!_networkManager)
            {
                PurrLogger.LogError($"Failed to start connection. {nameof(NetworkManager)} is null!", this);
                return;
            }

            if (_networkManager.transport is PurrTransport purrTransport)
            {
                // 직접 roomName 지정
                purrTransport.roomName = roomName;
            }

#if UTP_LOBBYRELAY
            else if (_networkManager.transport is UTPTransport utpTransport)
            {
                if (isHost)
                {
                    // Relay 서버 초기화 (Allocation 필요 → 따로 Relay 서비스에서 가져와야 함)
                    // utpTransport.InitializeRelayServer(myAllocation);
                }
                else
                {
                    // Relay 클라이언트 초기화 (JoinCode 필요)
                    // utpTransport.InitializeRelayClient("MyJoinCode123");
                }
            }
#else
            // P2P Connection, receive IP/Port from server
#endif

            if (isHost)
                _networkManager.StartServer();

            StartCoroutine(StartClient());
        }

        private IEnumerator StartClient()
        {
            yield return new WaitForSeconds(1f);
            _networkManager.StartClient();
        }
    }
}
