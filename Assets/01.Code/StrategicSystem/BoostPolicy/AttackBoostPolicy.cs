using Code.Command;
using Code.CoreGameLogic;
using UnityEngine;

namespace Code.StrategicSystem.BoostPolicy
{
    /// <summary>
    /// 특정 기물의 공격력을 증가시키는 정책 명령입니다.
    /// ICommand 인터페이스를 구현합니다.
    /// </summary>
    public class AttackBoostPolicy : ICommand
    {
        private readonly IPiece _targetPiece;
        private readonly int _boostAmount;
        private bool _isCompleted = false;

        public bool IsComplete => _isCompleted;

        /// <summary>
        /// AttackBoostPolicy의 생성자입니다.
        /// </summary>
        /// <param name="targetPiece">공격력을 증가시킬 대상 기물</param>
        /// <param name="boostAmount">증가시킬 공격력 수치</param>
        public AttackBoostPolicy(IPiece targetPiece, int boostAmount)
        {
            _targetPiece = targetPiece;
            _boostAmount = boostAmount;
        }

        /// <summary>
        /// 정책을 실행하여 대상 기물의 공격력을 증가시킵니다.
        /// </summary>
        public void Execute()
        {
            if (_targetPiece != null)
            {
                _targetPiece.ModifyAttackPower(_boostAmount);
                _isCompleted = true;
                Debug.Log($"정책: {_targetPiece.PieceName}의 공격력이 {_boostAmount}만큼 증가했습니다. 현재 공격력: {_targetPiece.AttackPower}");
            }
            else
                Debug.Log("정책: 공격력을 증가시킬 대상 기물이 유효하지 않습니다.");
        }

        /// <summary>
        /// 정책 실행을 되돌려 대상 기물의 공격력을 원래대로 되돌립니다.
        /// </summary>
        public void Undo()
        {
            if (_isCompleted && _targetPiece != null)
            {
                _targetPiece.ModifyAttackPower(-_boostAmount); // 증가된 공격력을 되돌립니다.
                _isCompleted = false;
                Debug.Log($"정책: {_targetPiece.PieceName}의 공격력 증가를 되돌렸습니다. 현재 공격력: {_targetPiece.AttackPower}");
            }
            else if (!_isCompleted)
                Debug.Log("되돌릴 공격력 증가 정책이 완료되지 않았거나 대상 기물이 유효하지 않습니다.");
        }
    }
}