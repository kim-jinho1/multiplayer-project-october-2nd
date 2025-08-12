using System.Collections.Generic;
using PurrNet;
using UnityEngine;
using PlayerID = Code.Players.PlayerID;

namespace Code.CoreGameLogic.Pieces
{
    /// <summary>
    /// 룩 기물을 나타내는 클래스입니다.
    /// </summary>
    public class Rook : Piece
    {
        public override string PieceName => new("Rook");

        public Rook(PlayerID ownerId, IPieceMoveValidator validator)
            : base(ownerId, validator)
        {
            Health.value = 100;
            AttackPower.value = 50;
            DefensePower.value = 20;
        }

        /// <summary>
        /// 룩이 이동 가능한 모든 수평 및 수직 위치를 계산하여 반환합니다.
        /// </summary>
        public override SyncVar<List<Vector2>> GetPossibleMoves(IBoard board, Vector2 currentPos)
        {
            var possibleMoves = new SyncVar<List<Vector2>>();
            for (int x = (int)currentPos.x + 1; x < board.BoardSize.x; x++)
            {
                Vector2 target = new Vector2(x, currentPos.y);
                if (Validator.IsValidMove(board, this, currentPos, target))
                {
                    possibleMoves.value.Add(target);
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