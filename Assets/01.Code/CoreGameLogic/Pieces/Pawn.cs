using System.Collections.Generic;
using PurrNet;
using UnityEngine;

namespace Code.CoreGameLogic.Pieces
{
    /// <summary>
    /// 폰 기물을 나타내는 클래스입니다.
    /// </summary>
    public class Pawn : Piece
    {
        public override string PieceName => new("Pawn");

        public Pawn(IPieceMoveValidator validator) : base(validator)
        {
            
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