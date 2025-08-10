using UnityEngine;

namespace Code.CoreGameLogic
{
    public class KnightMoveValidator : IPieceMoveValidator
    {
        public bool IsValidMove(IBoard board, Piece piece, Vector2 from, Vector2 to)
        {
            if (to.x < 0 || to.x >= board.BoardSize.x || to.y < 0 || to.y >= board.BoardSize.y)
                return false;
            
            float dx = Mathf.Abs(to.x - from.x);
            float dy = Mathf.Abs(to.y - from.y);

            // (dx=1, dy=2) 또는 (dx=2, dy=1) 여야 합니다.
            if (!((dx == 1 && dy == 2) || (dx == 2 && dy == 1)))
                return false;

            IPiece targetPiece = board.GetPieceAt(to);
            if (targetPiece != null && targetPiece.OwnerID == piece.OwnerID)
            {
                Debug.Log("나이트가 아군 기물이 있는 곳으로는 이동할 수 없습니다.");
                return false;
            }

            Debug.Log("나이트 이동 유효!");
            return true;
        }
    }
}