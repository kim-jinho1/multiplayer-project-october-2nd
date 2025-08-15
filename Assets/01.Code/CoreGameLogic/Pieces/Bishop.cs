using System.Collections.Generic;
using PurrNet;
using UnityEngine;
using PlayerID = Code.Players.PlayerID;

namespace Code.CoreGameLogic.Pieces
{
    /// <summary>
    /// 비숍 기물을 나타내는 클래스
    /// </summary>
    public class Bishop : Piece
    {
        public override string PieceName => "Bishop";
        
        public Bishop(PlayerID ownerId, IPieceMoveValidator validator, SyncVar<int> health, SyncVar<int> defensePower, SyncVar<int> attackPower, SyncVar<PlayerID> ownerID)
            : base(ownerId, validator, health, defensePower, attackPower, ownerID)
        {
            Health.value = PieceData.Health;
            AttackPower.value = PieceData.AttackPower;
            DefensePower.value = PieceData.DefensePower;
        }
        
        /// <summary>
        /// 비숍이 이동 가능한 모든 대각선 위치를 계산하여 반환합니다.
        /// </summary>
        public override SyncVar<List<Vector2>> GetPossibleMoves(IBoard board, Vector2 currentPos)
        {
            var possibleMoves = new SyncVar<List<Vector2>>();
            for (int i = 1; i < board.BoardSize.x; i++)
            {
                Vector2 target = new Vector2(currentPos.x + i, currentPos.y + i);
                if (Validator.IsValidMove(board, this, currentPos, target))
                {
                    possibleMoves.value.Add(target);
                }
                else 
                {
                    break;
                }
            }
            return possibleMoves;
        }
    }
}