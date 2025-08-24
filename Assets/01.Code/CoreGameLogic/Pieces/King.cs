using System.Collections.Generic;
using PurrNet;
using UnityEngine;

namespace Code.CoreGameLogic.Pieces
{
    public class King : Piece
    {
        public override string PieceName => "King";

        public King(IPieceMoveValidator validator) : base(validator)
        {

        }
        
        /// <summary>
        /// 킹의 이동 범위 (8방향 1칸)
        /// </summary>
        public override List<Vector3> GetMoveRange(Vector3 currentPos)
        {
            List<Vector3> moveRange = new List<Vector3>();
            int zMultiplier = IsForward ? -1 : 1;

            for (int xOffset = -1; xOffset <= 1; xOffset++)
            {
                for (int zOffset = -1; zOffset <= 1; zOffset++)
                {
                    if (xOffset == 0 && zOffset == 0) continue;
                    moveRange.Add(new Vector3(currentPos.x + xOffset, currentPos.y, currentPos.z + zOffset * zMultiplier));
                }
            }

            return moveRange;
        }
    }
}