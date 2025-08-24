using System.Collections.Generic;
using PurrNet;
using UnityEngine;

namespace Code.CoreGameLogic.Pieces
{
    public class Bishop : Piece
    {
        public override string PieceName => "Bishop";
        
        public Bishop(IPieceMoveValidator validator) : base(validator)
        {
           
        }
        
        /// <summary>
        /// 비숍의 대각선 이동 범위
        /// </summary>
        public override List<Vector3> GetMoveRange(Vector3 currentPos)
        {
            List<Vector3> moveRange = new List<Vector3>();
            Vector3[] directions = {
                new Vector3(1,0,1), new Vector3(-1,0,1),
                new Vector3(1,0,-1), new Vector3(-1,0,-1)
            };

            int zMultiplier = IsForward ? -1 : 1;

            foreach (Vector3 direction in directions)
            {
                Vector3 dir = new Vector3(direction.x, direction.y, direction.z * zMultiplier);
                for (int i = 1; i <= 7; i++)
                    moveRange.Add(currentPos + dir * i);
            }

            return moveRange;
        }

    }
}
