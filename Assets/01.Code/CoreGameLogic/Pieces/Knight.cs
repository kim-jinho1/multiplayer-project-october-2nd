using System.Collections.Generic;
using PurrNet;
using UnityEngine;

namespace Code.CoreGameLogic.Pieces
{
    public class Knight : Piece
    {
        public override string PieceName => "Knight";

        public Knight(IPieceMoveValidator validator) : base(validator)
        {
            
        }

        /// <summary>
        /// 나이트의 L자 이동 범위
        /// </summary>
        public override List<Vector3> GetMoveRange(Vector3 currentPos)
        {
            float zMultiplier = IsForward ? -1 : 1;

            Vector3[] knightMoves = {
                new(currentPos.x + 1, currentPos.y, currentPos.z + 2 * zMultiplier),
                new(currentPos.x + 1, currentPos.y, currentPos.z - 2 * zMultiplier),
                new(currentPos.x - 1, currentPos.y, currentPos.z + 2 * zMultiplier),
                new(currentPos.x - 1, currentPos.y, currentPos.z - 2 * zMultiplier),
                new(currentPos.x + 2, currentPos.y, currentPos.z + 1 * zMultiplier),
                new(currentPos.x + 2, currentPos.y, currentPos.z - 1 * zMultiplier),
                new(currentPos.x - 2, currentPos.y, currentPos.z + 1 * zMultiplier),
                new(currentPos.x - 2, currentPos.y, currentPos.z - 1 * zMultiplier)
            };

            return new List<Vector3>(knightMoves);
        }

    }
}