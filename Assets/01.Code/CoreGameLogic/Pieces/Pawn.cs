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
        // PieceName 속성을 오버라이드하여 폰의 이름을 반환합니다.
        public override string PieceName => "Pawn";

        public Pawn(PlayerID ownerId, IPieceMoveValidator validator)
            : base(ownerId, validator)
        {
            // Pawn의 기본 스탯을 설정합니다.
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
            
            // 폰의 이동 로직을 구현합니다.
            // 여기서는 PawnMoveValidator를 사용하여 유효성 검사를 진행합니다.

            // 예시: 앞으로 한 칸 이동
            Vector2 forwardMove = new Vector2(currentPos.x, currentPos.y + 1);
            if (_validator.IsValidMove(board, this, currentPos, forwardMove))
            {
                possibleMoves.Add(forwardMove);
            }
            
            // ... 다른 폰 이동 규칙(첫 턴 두 칸 이동, 대각선 공격) 로직 추가
            
            return possibleMoves;
        }
    }
}