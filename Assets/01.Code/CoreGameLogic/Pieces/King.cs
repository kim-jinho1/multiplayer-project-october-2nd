using System.Collections.Generic;
using PurrNet;
using UnityEngine;
using PlayerID = Code.Players.PlayerID;

namespace Code.CoreGameLogic.Pieces
{
    /// <summary>
    /// 킹 기물을 나타내는 클래스입니다.
    /// </summary>
    public class King : Piece
    {
        public override string PieceName => "King";

        public King(PlayerID ownerId, IPieceMoveValidator validator)
            : base(ownerId, validator)
        {
            Health.value = pieceData.Health;
            AttackPower.value = pieceData.AttackPower;
            DefensePower.value = pieceData.DefensePower;
        }

        /// <summary>
        /// 킹이 이동 가능한 모든 한 칸짜리 위치를 계산하여 반환합니다.
        /// </summary>
        public override SyncVar<List<Vector2>> GetPossibleMoves(IBoard board, Vector2 currentPos)
        {
            var possibleMoves = new SyncVar<List<Vector2>>();
            
            for (int xOffset = -1; xOffset <= 1; xOffset++)
            {
                for (int yOffset = -1; yOffset <= 1; yOffset++)
                {
                    if (xOffset == 0 && yOffset == 0) continue;

                    Vector2 target = new Vector2(currentPos.x + xOffset, currentPos.y + yOffset);
                    if (Validator.IsValidMove(board, this, currentPos, target))
                    {
                        possibleMoves.value.Add(target);
                    }
                }
            }
            return possibleMoves;
        }
    }
}