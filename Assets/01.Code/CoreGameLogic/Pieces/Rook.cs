using System.Collections.Generic;
using Code.Players;
using UnityEngine;

namespace Code.CoreGameLogic.Pieces
{
    /// <summary>
    /// 룩 기물을 나타내는 클래스입니다.
    /// Piece 추상 클래스를 상속받아 룩의 고유한 로직을 구현합니다.
    /// </summary>
    public class Rook : Piece
    {
        public override string PieceName => "Rook";

        public Rook(PlayerID ownerId, IPieceMoveValidator validator)
            : base(ownerId, validator)
        {
            Health = 100;
            AttackPower = 50;
            DefensePower = 20;
        }

        /// <summary>
        /// 룩이 이동 가능한 모든 수평 및 수직 위치를 계산하여 반환합니다.
        /// </summary>
        public override List<Vector2> GetPossibleMoves(IBoard board, Vector2 currentPos)
        {
            var possibleMoves = new List<Vector2>();
            for (int x = (int)currentPos.x + 1; x < board.BoardSize.x; x++)
            {
                Vector2 target = new Vector2(x, currentPos.y);
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