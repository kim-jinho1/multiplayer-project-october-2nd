using UnityEngine;

namespace Code.CoreGameLogic
{
    public class ChessBoard
    {
        private readonly Piece[,] _pieces;
        public int BoardSize => 8;

        public ChessBoard()
        {
            _pieces = new Piece[BoardSize, BoardSize];
        }

        public Piece GetPieceAt(Vector2 position)
        {
            int x = (int)position.x;
            int y = (int)position.y;
            
            if (x >= 0 && x < BoardSize && y >= 0 && y < BoardSize)
            {
                return _pieces[x, y];
            }
            return null;
        }

        public void PlacePiece(Piece piece, Vector2 position)
        {
            _pieces[(int)position.x, (int)position.y] = piece;
        }

        public void MovePiece(Vector2 from, Vector2 to)
        {
            var piece = GetPieceAt(from);
            if (piece != null)
            {
                PlacePiece(null, from); // 기존 위치를 비움
                PlacePiece(piece, to);   // 새 위치에 기물을 놓음
            }
        }
    }
}