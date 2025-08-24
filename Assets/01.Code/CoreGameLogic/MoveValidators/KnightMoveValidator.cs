using UnityEngine;

namespace Code.CoreGameLogic
{
    public class KnightMoveValidator : IPieceMoveValidator
    {
        public bool IsValidMove(Piece piece, Vector3 from, Vector3 to)
        {
            return false;
        }
    }
}