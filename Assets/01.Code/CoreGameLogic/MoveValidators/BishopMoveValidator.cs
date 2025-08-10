using UnityEngine;
using System.Collections.Generic;

namespace Code.CoreGameLogic
{
    public class BishopMoveValidator : IPieceMoveValidator
    {
        public bool IsValidMove(IBoard board, Piece piece, Vector2 from, Vector2 to)
        {
            if (to.x < 0 || to.x >= board.BoardSize.x || to.y < 0 || to.y >= board.BoardSize.y)
                return false;

            float dx = to.x - from.x;
            float dy = to.y - from.y;

            if (Mathf.Abs(dx) != Mathf.Abs(dy) || dx == 0)
                return false;
            
            Vector2 direction = new Vector2(Mathf.Sign(dx), Mathf.Sign(dy));
            Vector2 currentCheckPos = from + direction;

            while (currentCheckPos != to)
            {
                if (board.GetPieceAt(currentCheckPos) != null)
                {
                    Debug.Log("비숍 경로에 장애물이 있습니다.");
                    return false;
                }
                currentCheckPos += direction;
            }
            
            IPiece targetPiece = board.GetPieceAt(to);
            if (targetPiece != null && targetPiece.OwnerID == piece.OwnerID)
            {
                Debug.Log("비숍이 아군 기물이 있는 곳으로는 이동할 수 없습니다.");
                return false;
            }

            Debug.Log("비숍 이동 유효!");
            return true;
        }
    }
}