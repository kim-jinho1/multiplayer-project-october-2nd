using UnityEngine;
using PurrNet;
using Code.Command;
using Code.Command.Commands;

namespace Code.CoreGameLogic
{
    public class PieceTurnProcessor : NetworkBehaviour, ITurnProcessor
    {
        private GameManager _gameManager;

        public void Initialize(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void ProcessTurn()
        {
            if (_gameManager.currentPlayerId.value == Players.PlayerID.Player1 && networkManager.isHost)
            {
                Debug.Log($"기물 턴을 처리합니다: {_gameManager.currentPlayerId.value}");
            }
            else if (_gameManager.currentPlayerId.value == Players.PlayerID.Player2 && networkManager.isClient)
            {
                Debug.Log($"상대방의 기물 턴: {_gameManager.currentPlayerId.value}");
            }
        }

        [ServerRpc]
        public void MovePieceServerRpc(Piece piece, Vector3 destination)
        {
            if (piece.OwnerID == _gameManager.currentPlayerId.value)
            {
                ICommand moveCommand = new MoveCommand(piece, destination);
                _gameManager.ExecuteCommand(moveCommand);
            }
        }

        [ServerRpc]
        public void AttackPieceServerRpc(Piece attacker, Piece defender)
        {
            if (attacker.OwnerID == _gameManager.currentPlayerId.value)
            {
                ICommand attackCommand = new AttackCommand(attacker, defender);
                _gameManager.ExecuteCommand(attackCommand);
            }
        }
    }
}