using System.Collections.Generic;
using PurrNet;
using UnityEngine;

namespace Code.CoreGameLogic.Pieces
{
    public class Queen : Piece
    {
        public override string PieceName => "Queen";

        public Queen(IPieceMoveValidator validator) : base(validator)
        {
            
        }
        
        /// <summary>
        /// 퀸의 이동 범위 (8방향)
        /// </summary>
        public override List<Vector3> GetMoveRange(Vector3 currentPos)
        {
            List<Vector3> moveRange = new List<Vector3>();
    
            Vector3[] directions = {
                new Vector3(0,0,1), new Vector3(0,0,-1), new Vector3(1,0,0), new Vector3(-1,0,0),
                new Vector3(1,0,1), new Vector3(-1,0,1), new Vector3(1,0,-1), new Vector3(-1,0,-1)
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
