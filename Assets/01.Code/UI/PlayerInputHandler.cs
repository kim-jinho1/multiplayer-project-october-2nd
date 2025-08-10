using UnityEngine;
using Code.Command;
using Code.Command.Commands;
using Code.CoreGameLogic;
using Code.Global;

namespace Code.UI
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private GameManager _gameManager;
        private IPiece _selectedPiece;
        private Vector2 _selectedPosition;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        public void HandlePieceClick(PieceView pieceView)
        {
            if (_selectedPiece == pieceView.LogicalPiece)
            {
                _selectedPiece = null;
                Debug.Log("선택 해제");
            }
            else
            {
                _selectedPiece = pieceView.LogicalPiece;
                _selectedPosition = pieceView.CurrentPosition;
                EventManager.OnPieceSelected.Invoke(_selectedPiece);
                Debug.Log($"{_selectedPiece.PieceName} 선택됨!");
            }
        }

        public void HandleMoveInput(Vector2 targetPos)
        {
            if (_selectedPiece != null)
            {
                ICommand moveCommand = new MoveCommand(_gameManager.Board, _selectedPosition, targetPos);
                _gameManager.ExecuteCommand(moveCommand);
                _selectedPiece = null;
            }
        }
    }
}