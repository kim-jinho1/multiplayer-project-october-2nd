using Code.CoreGameLogic;
using UnityEngine;

namespace Code.Command.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly IBoard _board;
        private readonly Vector2 _from;
        private readonly Vector2 _to;
        private bool _isCompleted = false;

        public bool IsComplete => _isCompleted;

        // PlayerInputHandler에서 전달하는 3개의 인자를 받는 생성자
        public MoveCommand(IBoard board, Vector2 from, Vector2 to)
        {
            _board = board;
            _from = from;
            _to = to;
        }

        public void Execute()
        {
            // 이동 로직: 보드에서 기물을 이동시키고, 완료 상태를 true로 변경
            if (_board.MovePiece(_from, _to))
            {
                Debug.Log($"기물 {_from}에서 {_to}로 이동 완료");
                _isCompleted = true;
            }
            else
            {
                Debug.Log("이동할 수 없는 위치입니다.");
            }
        }

        public void Undo()
        {
            // 되돌리기 로직: 기물을 원래 위치로 되돌립니다.
            if (_isCompleted)
            {
                _board.MovePiece(_to, _from);
                _isCompleted = false;
                Debug.Log($"이동을 되돌려 {_to}에서 {_from}으로 복귀");
            }
        }
    }
}