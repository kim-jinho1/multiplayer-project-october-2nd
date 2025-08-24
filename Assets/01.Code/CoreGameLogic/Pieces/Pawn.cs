using System.Collections.Generic;
using PurrNet;
using UnityEngine;

namespace Code.CoreGameLogic.Pieces
{
    /// <summary>
    /// 폰 기물
    /// </summary>
    public class Pawn : Piece
    {
        public override string PieceName => "Pawn";

        public Pawn(IPieceMoveValidator validator) : base(validator)
        {
            
        }
        
        /// <summary>
        /// 폰의 기본 이동 범위 (앞으로 1칸)
        /// </summary>
        public override List<Vector3> GetMoveRange(Vector3 currentPos)
        {
            float zOffset = IsForward ? -1 : 1;
            return new List<Vector3> { new(currentPos.x, currentPos.y, currentPos.z + zOffset) };
        }

    }
}
