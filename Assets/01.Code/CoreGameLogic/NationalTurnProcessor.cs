using UnityEngine;
using Code.Players;
using PurrNet;
using PlayerID = Code.Players.PlayerID;

namespace Code.CoreGameLogic
{
    public class NationalTurnProcessor : NetworkBehaviour, ITurnProcessor
    {
        [SerializeField] private GameObject _nationalUI;
        
        private GameManager _gameManager;
        
        public void Initialize(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        [ServerRpc]
        private void AddResourcesServerRpc(PlayerID playerID)
        {
            if (_gameManager.players.value.ContainsKey(playerID))
            {
                PlayerData playerData = _gameManager.players.value[playerID];

                playerData.AddGold(100);
                playerData.AddAP(10);
                
                if (_gameManager.currentPlayerId.value == PlayerID.Player2)
                {
                    playerData.AddGold(30);
                    playerData.AddAP(3);
                }

                Debug.Log($"{playerID}가 리소스(골드, AP)를 획득했습니다. 현재 골드: {playerData.Gold}");

                UpdatePlayerDataClientRpc(playerID, playerData.Gold, playerData.AP);
            }
        }
        
        
        [ObserversRpc]
        private void UpdatePlayerDataClientRpc(PlayerID playerID, int gold, int ap)
        {
            if (_gameManager.players.value.ContainsKey(playerID))
            {
                _gameManager.players.value[playerID].AddGold(gold);
                _gameManager.players.value[playerID].AddAP(ap);
            }
        }

        public void ProcessTurn()
        {
            if (_gameManager.currentPlayerId.value == PlayerID.Player1 && networkManager.isHost)
            {
                OpenUI();
                AddResourcesServerRpc(_gameManager.currentPlayerId.value);
            }
            else if (_gameManager.currentPlayerId.value == PlayerID.Player2 && networkManager.isClient)
            {
                OpenUI();
                AddResourcesServerRpc(_gameManager.currentPlayerId.value);
            }
            else
            {
                _nationalUI.SetActive(false);
            }
        }
        
        public void OnEndTurnButtonClicked()
        {
            EndTurnServerRpc();
        }
        
        [ServerRpc]
        public void EndTurnServerRpc()
        {
            _gameManager.EndCurrentTurn();
        }

        public void OpenUI()
        {
            _nationalUI.SetActive(true);
        }
    }
}