using System.Collections.Generic;
using Code.Players;
using UnityEngine;

namespace Code.CoreGameLogic.Pieces
{
    /// <summary>
    /// 킹 기물을 나타내는 클래스입니다.
    /// Piece 추상 클래스를 상속받아 킹의 고유한 로직을 구현합니다.
    /// </summary>
    public class King : Piece
    {
        public override string PieceName => "King";

        public King(PlayerID ownerId, IPieceMoveValidator validator)
            : base(ownerId, validator)
        {
            Health = 100; // 킹의 체력은 일반적으로 중요하지 않지만, 여기서는 예시로 추가
            AttackPower = 10;
            DefensePower = 50; // 킹은 방어력이 중요
        }

        /// <summary>
        /// 킹이 이동 가능한 모든 한 칸짜리 위치를 계산하여 반환합니다.
        /// </summary>
        public override List<Vector2> GetPossibleMoves(IBoard board, Vector2 currentPos)
        {
            var possibleMoves = new List<Vector2>();

            // 킹의 한 칸 이동 로직 (상하좌우, 대각선)을 여기에 구현합니다.
            // 예시: 주변 8방향 확인
            for (int xOffset = -1; xOffset <= 1; xOffset++)
            {
                for (int yOffset = -1; yOffset <= 1; yOffset++)
                {
                    if (xOffset == 0 && yOffset == 0) continue; // 현재 위치 제외

                    Vector2 target = new Vector2(currentPos.x + xOffset, currentPos.y + yOffset);
                    if (_validator.IsValidMove(board, this, currentPos, target))
                    {
                        possibleMoves.Add(target);
                    }
                }
            }
            // ... 캐슬링 로직 등 특수 이동 로직 추가

            return possibleMoves;
        }
    }
}