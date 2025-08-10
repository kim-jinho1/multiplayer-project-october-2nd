using UnityEngine;

namespace Code.CoreGameLogic
{
    public class RookMoveValidator : IPieceMoveValidator
    {
        public bool IsValidMove(IBoard board, Piece piece, Vector2 from, Vector2 to)
        {
            if (to.x < 0 || to.x >= board.BoardSize.x || to.y < 0 || to.y >= board.BoardSize.y)
                return false;
            
            bool isStraightHorizontal = (to.y == from.y && to.x != from.x);
            bool isStraightVertical = (to.x == from.x && to.y != from.y);

            if (!isStraightHorizontal && !isStraightVertical)
            {
                return false;
            }

            Vector2 direction = Vector2.zero;
            if (isStraightHorizontal)
            {
                direction = new Vector2(Mathf.Sign(to.x - from.x), 0);
            }
            else
                direction = new Vector2(0, Mathf.Sign(to.y - from.y));

            Vector2 currentCheckPos = from + direction;
            while (currentCheckPos != to)
            {
                if (board.GetPieceAt(currentCheckPos) != null)
                {
                    Debug.Log("룩 경로에 장애물이 있습니다.");
                    return false;
                }
                currentCheckPos += direction;
            }
            
            IPiece targetPiece = board.GetPieceAt(to);
            if (targetPiece != null && targetPiece.OwnerID == piece.OwnerID)
            {
                Debug.Log("룩이 아군 기물이 있는 곳으로는 이동할 수 없습니다.");
                return false;
            }

            Debug.Log("룩 이동 유효!");
            return true;
        }
    }
}