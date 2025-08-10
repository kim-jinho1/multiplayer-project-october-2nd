using UnityEngine;

namespace Code.CoreGameLogic
{
    public class PawnMoveValidator : IPieceMoveValidator
    {
        public bool IsValidMove(IBoard board, Piece piece, Vector2 from, Vector2 to)
        {

            if (Mathf.Approximately(to.y, from.y + 1) && Mathf.Approximately(to.x, from.x))
            {
                return board.GetPieceAt(to) == null;
            }

            return false;
        }
    }
}