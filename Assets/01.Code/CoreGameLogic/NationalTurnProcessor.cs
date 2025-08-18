using UnityEngine;
using Code.Global;
using Code.Players;
using PurrNet;
using PlayerID = PurrNet.PlayerID;

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
        private void AddResources(PlayerData playerData)
        {
            playerData.AddGold(100);
            playerData.AddAP(10);
        }
        
        [ObserversRpc]
        public void EndTurn(PlayerID playerID)
        {
            _gameManager.EndCurrentTurn();
            _nationalUI.SetActive(false);
        }

        public void OpenUI()
        {
            _nationalUI.SetActive(true);
        }

        public void ProcessTurn()
        {
            _gameManager = DependencyContainer.Get<GameManager>();
            PlayerData currentPlayerData = _gameManager.players.value[_gameManager.currentPlayerId.value];
            
            AddResources(currentPlayerData);
        }
    }
}