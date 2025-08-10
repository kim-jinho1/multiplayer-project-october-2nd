using UnityEngine;

namespace Code.CoreGameLogic
{
    public class QueenMoveValidator : IPieceMoveValidator
    {
        public bool IsValidMove(IBoard board, Piece piece, Vector2 from, Vector2 to)
        {
            if (to.x < 0 || to.x >= board.BoardSize.x || to.y < 0 || to.y >= board.BoardSize.y)
                return false;

            float dx = to.x - from.x;
            float dy = to.y - from.y;
            
            bool isStraight = (dx == 0 && dy != 0) || (dy == 0 && dx != 0);
            bool isDiagonal = (Mathf.Abs(dx) == Mathf.Abs(dy) && dx != 0); 

            if (!isStraight && !isDiagonal)
                return false;
            
            Vector2 direction = Vector2.zero;
            if (isStraight)
            {
                if (dx == 0) direction = new Vector2(0, Mathf.Sign(dy));
                else direction = new Vector2(Mathf.Sign(dx), 0);
            }
            else
                direction = new Vector2(Mathf.Sign(dx), Mathf.Sign(dy));

            Vector2 currentCheckPos = from + direction;
            while (currentCheckPos != to)
            {
                if (board.GetPieceAt(currentCheckPos) != null)
                {
                    Debug.Log("퀸 경로에 장애물이 있습니다.");
                    return false;
                }
                currentCheckPos += direction;
            }

            IPiece targetPiece = board.GetPieceAt(to);
            if (targetPiece != null && targetPiece.OwnerID == piece.OwnerID)
            {
                Debug.Log("퀸이 아군 기물이 있는 곳으로는 이동할 수 없습니다.");
                return false;
            }

            Debug.Log("퀸 이동 유효!");
            return true;
        }
    }
}