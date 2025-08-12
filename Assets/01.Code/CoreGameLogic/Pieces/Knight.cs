using System.Collections.Generic;
using PurrNet;
using UnityEngine;
using PlayerID = Code.Players.PlayerID;

namespace Code.CoreGameLogic.Pieces
{
    /// <summary>
    /// 나이트 기물을 나타내는 클래스입니다.
    /// </summary>
    public class Knight : Piece
    {
        public override string PieceName => "Knight";

        public Knight(PlayerID ownerId, IPieceMoveValidator validator)
            : base(ownerId, validator)
        {
            Health.value = 100;
            AttackPower.value = 25;
            DefensePower.value = 15;
        }

        /// <summary>
        /// 나이트가 이동 가능한 모든 'L'자형 위치를 계산하여 반환합니다.
        /// </summary>
        public override SyncVar<List<Vector2>> GetPossibleMoves(IBoard board, Vector2 currentPos)
        {
            var possibleMoves = new SyncVar<List<Vector2>>();
            Vector2[] knightMoves = {
                new(currentPos.x + 1, currentPos.y + 2),
                new(currentPos.x + 1, currentPos.y - 2),
                new(currentPos.x - 1, currentPos.y + 2),
                new(currentPos.x - 1, currentPos.y - 2),
                new(currentPos.x + 2, currentPos.y + 1),
                new(currentPos.x + 2, currentPos.y - 1),
                new(currentPos.x - 2, currentPos.y + 1),
                new(currentPos.x - 2, currentPos.y - 1)
            };

            foreach (Vector2 target in knightMoves)
            {
                if (Validator.IsValidMove(board, this, currentPos, target))
                {
                    possibleMoves.value.Add(target);
                }
            }

            return possibleMoves;
        }
    }
}