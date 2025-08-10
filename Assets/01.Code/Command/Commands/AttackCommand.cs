using Code.CoreGameLogic;
using UnityEngine;

namespace Code.Command.Commands
{
    public class AttackCommand : ICommand
    {
        private readonly IBoard _board;
        private readonly IBattleResolver _battleResolver;
        private readonly Piece _attacker;
        private readonly Piece _defender;
        private bool _isCompleted = false;
        
        public bool IsComplete => _isCompleted;

        public AttackCommand(IBoard board, IBattleResolver battleResolver, Piece attacker, Piece defender)
        {
            _board = board;
            _battleResolver = battleResolver;
            _attacker = attacker;
            _defender = defender;
        }

        public void Execute()
        {
            // 전투를 처리하고 결과를 반환합니다.
            _battleResolver.ResolveBattle(_attacker, _defender);
            _isCompleted = true;
            Debug.Log($"공격 명령 실행: {_attacker.PieceName}가 {_defender.PieceName}를 공격했습니다.");
        }

        public void Undo()
        {
            // 전투는 되돌릴 수 없으므로 로그만 출력
            Debug.Log("공격 명령은 되돌릴 수 없습니다.");
        }
    }
}