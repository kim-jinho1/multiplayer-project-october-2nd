using System.Collections.Generic;
using Code.Players;
using UnityEngine;

namespace Code.CoreGameLogic.Pieces
{
    /// <summary>
    /// 나이트 기물을 나타내는 클래스입니다.
    /// Piece 추상 클래스를 상속받아 나이트의 고유한 로직을 구현합니다.
    /// </summary>
    public class Knight : Piece
    {
        public override string PieceName => "Knight";

        public Knight(PlayerID ownerId, IPieceMoveValidator validator)
            : base(ownerId, validator)
        {
            Health = 100;
            AttackPower = 25;
            DefensePower = 15;
        }

        /// <summary>
        /// 나이트가 이동 가능한 모든 'L'자형 위치를 계산하여 반환합니다.
        /// </summary>
        public override List<Vector2> GetPossibleMoves(IBoard board, Vector2 currentPos)
        {
            var possibleMoves = new List<Vector2>();

            // 나이트의 8가지 'L'자형 이동 패턴을 정의합니다.
            Vector2[] knightMoves = new Vector2[]
            {
                new Vector2(currentPos.x + 1, currentPos.y + 2),
                new Vector2(currentPos.x + 1, currentPos.y - 2),
                new Vector2(currentPos.x - 1, currentPos.y + 2),
                new Vector2(currentPos.x - 1, currentPos.y - 2),
                new Vector2(currentPos.x + 2, currentPos.y + 1),
                new Vector2(currentPos.x + 2, currentPos.y - 1),
                new Vector2(currentPos.x - 2, currentPos.y + 1),
                new Vector2(currentPos.x - 2, currentPos.y - 1)
            };

            foreach (Vector2 target in knightMoves)
            {
                if (_validator.IsValidMove(board, this, currentPos, target))
                {
                    possibleMoves.Add(target);
                }
            }

            return possibleMoves;
        }
    }
}