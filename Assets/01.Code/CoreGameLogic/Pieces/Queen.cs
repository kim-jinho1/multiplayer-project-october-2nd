using System.Collections.Generic;
using Code.Players;
using UnityEngine;

namespace Code.CoreGameLogic.Pieces
{
    /// <summary>
    /// 퀸 기물을 나타내는 클래스입니다.
    /// Piece 추상 클래스를 상속받아 퀸의 고유한 로직을 구현합니다.
    /// </summary>
    public class Queen : Piece
    {
        public override string PieceName => "Queen";

        public Queen(PlayerID ownerId, IPieceMoveValidator validator)
            : base(ownerId, validator)
        {
            Health = 100;
            AttackPower = 90;
            DefensePower = 30;
        }

        /// <summary>
        /// 퀸이 이동 가능한 모든 직선 및 대각선 위치를 계산하여 반환합니다.
        /// (룩과 비숍의 이동 로직을 합친 것과 같습니다)
        /// </summary>
        public override List<Vector2> GetPossibleMoves(IBoard board, Vector2 currentPos)
        {
            var possibleMoves = new List<Vector2>();

            // 퀸의 이동 로직 (상하좌우, 대각선)을 여기에 구현합니다.
            // 룩과 비숍의 GetPossibleMoves 로직을 조합하여 구현할 수 있습니다.
            
            // 예시: 수평 이동 (오른쪽)
            for (int x = (int)currentPos.x + 1; x < board.BoardSize.x; x++)
            {
                Vector2 target = new Vector2(x, currentPos.y);
                if (_validator.IsValidMove(board, this, currentPos, target))
                {
                    possibleMoves.Add(target);
                }
                else break;
            }
            // ... 다른 7방향 (왼쪽, 위, 아래, 4개 대각선)에 대한 로직 추가

            return possibleMoves;
        }
    }
}