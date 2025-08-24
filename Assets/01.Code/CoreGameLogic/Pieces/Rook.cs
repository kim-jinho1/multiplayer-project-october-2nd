using System.Collections.Generic;
using PurrNet;
using UnityEngine;

namespace Code.CoreGameLogic.Pieces
{
    public class Rook : Piece
    {
        public override string PieceName => "Rook";

        public Rook(IPieceMoveValidator validator) : base(validator)
        {
            
        }
        
        /// <summary>
        /// 룩의 직선 이동 범위 (상하좌우)
        /// </summary>
        public override List<Vector3> GetMoveRange(Vector3 currentPos)
        {
            List<Vector3> moveRange = new List<Vector3>();
    
            Vector3[] directions = {
                new(0, 0, 1),
                new(0, 0, -1),
                new(1, 0, 0),
                new(-1, 0, 0)
            };

            foreach (Vector3 direction in directions)
            {
                Vector3 dir = IsForward ? new Vector3(direction.x, direction.y, -direction.z) : direction;

                for (int i = 1; i <= 7; i++)
                    moveRange.Add(currentPos + dir * i);
            }

            return moveRange;
        }
    }
}