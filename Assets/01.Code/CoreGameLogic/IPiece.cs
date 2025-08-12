using System.Collections.Generic;
using PurrNet;
using UnityEngine;
using PlayerID = Code.Players.PlayerID;

namespace Code.CoreGameLogic
{
    /// <summary>
    /// 모든 체스 기물이 가져야 할 공통 기능과 속성을 정의하는 인터페이스
    /// </summary>
    public interface IPiece
    {
        string PieceName { get; }
        SyncVar<PlayerID> OwnerID { get; }
        SyncVar<int> AttackPower { get; }
        SyncVar<int> DefensePower { get; }
        SyncVar<int> Health { get; }
        SyncVar<bool> IsAlive { get; }

        SyncVar<List<Vector2>> GetPossibleMoves(IBoard board, Vector2 currentPos);
        
        void ModifyAttackPower(int amount);
    }
}