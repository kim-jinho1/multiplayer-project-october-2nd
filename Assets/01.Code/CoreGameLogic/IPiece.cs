using System.Collections.Generic;
using Code.Players;
using UnityEngine;

namespace Code.CoreGameLogic
{
    /// <summary>
    /// 모든 체스 기물이 가져야 할 공통 기능과 속성을 정의하는 인터페이스
    /// </summary>
    public interface IPiece
    {
        string PieceName { get; }
        PlayerID OwnerID { get; }
        int AttackPower { get; }
        int DefensePower { get; }
        int Health { get; }
        bool IsAlive { get; }

        List<Vector2> GetPossibleMoves(IBoard board, Vector2 currentPos);
        
        void ModifyAttackPower(int amount);
    }
}