using System.Collections.Generic;
using PurrNet;
using UnityEngine;
using PlayerID = Code.Players.PlayerID;

namespace Code.CoreGameLogic.Pieces
{
    /// <summary>
    /// 폰 기물을 나타내는 클래스입니다.
    /// </summary>
    public class Pawn : Piece
    {
        public override string PieceName => new("Pawn");

        public Pawn(PlayerID ownerId, IPieceMoveValidator validator, SyncVar<int> health, SyncVar<int> defensePower, SyncVar<int> attackPower, SyncVar<PlayerID> ownerID)
            : base(ownerId, validator, health, defensePower, attackPower, ownerID)
        {
            Health.value = PieceData.Health;
            AttackPower.value = PieceData.AttackPower;
            DefensePower.value = PieceData.DefensePower;
        }
        
        /// <summary>
        /// 폰이 이동 가능한 모든 위치를 계산하여 반환합니다.
        /// </summary>
        public override SyncVar<List<Vector2>> GetPossibleMoves(IBoard board, Vector2 currentPos)
        {
            var possibleMoves = new SyncVar<List<Vector2>>();
            
            Vector2 forwardMove = new Vector2(currentPos.x, currentPos.y + 1);
            if (Validator.IsValidMove(board, this, currentPos, forwardMove))
            {
                possibleMoves.value.Add(forwardMove);
            }
            
            return possibleMoves;
        }
    }
}