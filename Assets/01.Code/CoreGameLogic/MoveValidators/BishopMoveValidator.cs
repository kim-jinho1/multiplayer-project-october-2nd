using UnityEngine;
using System.Collections.Generic;

namespace Code.CoreGameLogic
{
    public class BishopMoveValidator : IPieceMoveValidator
    {
        public bool IsValidMove(Piece piece, Vector3 from, Vector3 to)
        {
            return false;
        }
    }
}