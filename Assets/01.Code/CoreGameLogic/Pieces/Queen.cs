using System.Collections.Generic;
using PurrNet;
using UnityEngine;
using PlayerID = Code.Players.PlayerID;

namespace Code.CoreGameLogic.Pieces
{
    /// <summary>
    /// 퀸 기물을 나타내는 클래스입니다.
    /// </summary>
    public class Queen : Piece
    {
        public override string PieceName => new("Queen");

        public Queen(PlayerID ownerId, IPieceMoveValidator validator)
            : base(ownerId, validator)
        {
            Health.value = pieceData.Health;
            AttackPower.value = pieceData.AttackPower;
            DefensePower.value = pieceData.DefensePower;
        }

        /// <summary>
        /// 퀸이 이동 가능한 모든 직선 및 대각선 위치를 계산하여 반환합니다.
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
                else break;
            }
            return possibleMoves;
        }
    }
}