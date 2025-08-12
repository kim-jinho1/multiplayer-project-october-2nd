using System.Collections.Generic;
using Code.Players;
using UnityEngine;

namespace Code.CoreGameLogic.Pieces
{
    /// <summary>
    /// 비숍 기물을 나타내는 클래스
    /// </summary>
    public class Bishop : Piece
    {
        public override string PieceName => "Bishop";

        public Bishop(PlayerID ownerId, IPieceMoveValidator validator)
            : base(ownerId, validator)
        {
            Health = 100;
            AttackPower = 30;
            DefensePower = 10;
        }

        /// <summary>
        /// 비숍이 이동 가능한 모든 대각선 위치를 계산하여 반환합니다.
        /// </summary>
        public override List<Vector2> GetPossibleMoves(IBoard board, Vector2 currentPos)
        {
            var possibleMoves = new List<Vector2>();
            for (int i = 1; i < board.BoardSize.x; i++)
            {
                Vector2 target = new Vector2(currentPos.x + i, currentPos.y + i);
                if (_validator.IsValidMove(board, this, currentPos, target))
                {
                    possibleMoves.Add(target);
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