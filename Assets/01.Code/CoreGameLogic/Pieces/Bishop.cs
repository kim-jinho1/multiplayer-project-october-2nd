using System.Collections.Generic;
using Code.Players;
using UnityEngine;

namespace Code.CoreGameLogic.Pieces
{
    /// <summary>
    /// 비숍 기물을 나타내는 클래스입니다.
    /// Piece 추상 클래스를 상속받아 비숍의 고유한 로직을 구현합니다.
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

            // 비숍의 대각선 이동 로직을 여기에 구현합니다.
            // _validator.IsValidMove를 사용하여 각 대각선 방향으로 이동 가능한지 확인합니다.
            // 예시: 오른쪽 위 대각선
            for (int i = 1; i < board.BoardSize.x; i++) // 보드 크기에 따라 조정
            {
                Vector2 target = new Vector2(currentPos.x + i, currentPos.y + i);
                if (_validator.IsValidMove(board, this, currentPos, target))
                {
                    possibleMoves.Add(target);
                }
                else // 중간에 다른 기물이 막고 있다면 더 이상 진행 불가
                {
                    break;
                }
            }
            // ... 다른 세 대각선 방향에 대한 로직 추가 (오른쪽 아래, 왼쪽 위, 왼쪽 아래)

            return possibleMoves;
        }
    }
}