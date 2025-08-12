using System.Collections.Generic;
using Code.Players;
using UnityEngine;

namespace Code.CoreGameLogic.Pieces
{
    /// <summary>
    /// 폰 기물을 나타내는 클래스입니다.
    /// Piece 추상 클래스를 상속받아 폰의 고유한 로직을 구현합니다.
    /// </summary>
    public class Pawn : Piece
    {
        public override string PieceName => "Pawn";

        public Pawn(PlayerID ownerId, IPieceMoveValidator validator)
            : base(ownerId, validator)
        {
            Health = 100;
            AttackPower = 10;
            DefensePower = 5;
        }
        
        /// <summary>
        /// 폰이 이동 가능한 모든 위치를 계산하여 반환합니다.
        /// </summary>
        public override List<Vector2> GetPossibleMoves(IBoard board, Vector2 currentPos)
        {
            var possibleMoves = new List<Vector2>();
            
            Vector2 forwardMove = new Vector2(currentPos.x, currentPos.y + 1);
            if (_validator.IsValidMove(board, this, currentPos, forwardMove))
            {
                possibleMoves.Add(forwardMove);
            }
            
            return possibleMoves;
        }
    }
}