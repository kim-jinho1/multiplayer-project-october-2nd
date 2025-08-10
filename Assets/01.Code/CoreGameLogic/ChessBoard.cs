using UnityEngine;

namespace Code.CoreGameLogic
{
    public class ChessBoard : IBoard
    {
        // IPiece 타입의 2차원 배열로 변경하여 유연성을 높입니다.
        private readonly IPiece[,] _pieces;
        
        // BoardSize를 Vector2 타입으로 구현합니다. (예: 8x8 보드)
        public Vector2 BoardSize { get; } = new Vector2(8, 8);

        public ChessBoard()
        {
            _pieces = new IPiece[(int)BoardSize.x, (int)BoardSize.y];
            // 보드 초기화 로직 (예: 시작 기물 배치)은 여기에 추가될 수 있습니다.
        }

        /// <summary>
        /// 특정 위치에 있는 기물을 가져옵니다.
        /// </summary>
        /// <param name="position">확인할 보드 위치</param>
        /// <returns>해당 위치의 IPiece 객체, 없으면 null</returns>
        public IPiece GetPieceAt(Vector2 position)
        {
            int x = (int)position.x;
            int y = (int)position.y;
            
            // 보드 범위 검사
            if (x >= 0 && x < BoardSize.x && y >= 0 && y < BoardSize.y)
            {
                return _pieces[x, y];
            }
            return null;
        }

        /// <summary>
        /// 특정 위치에 기물을 배치합니다.
        /// </summary>
        /// <param name="piece">배치할 기물 객체 (null은 해당 칸을 비움)</param>
        /// <param name="position">기물을 배치할 보드 위치</param>
        public void PlacePiece(IPiece piece, Vector2 position)
        {
            int x = (int)position.x;
            int y = (int)position.y;

            if (x >= 0 && x < BoardSize.x && y >= 0 && y < BoardSize.y)
            {
                _pieces[x, y] = piece;
            }
            else
            {
                Debug.LogError($"보드 범위({BoardSize.x},{BoardSize.y})를 벗어난 위치에 기물을 배치하려고 시도했습니다: {position}");
            }
        }

        /// <summary>
        /// 보드에서 기물을 이동시킵니다.
        /// </summary>
        /// <param name="from">기물이 있는 시작 위치</param>
        /// <param name="to">기물이 이동할 목표 위치</param>
        /// <returns>이동 성공 여부 (true: 성공, false: 실패)</returns>
        public bool MovePiece(Vector2 from, Vector2 to)
        {
            IPiece pieceToMove = GetPieceAt(from);
            
            if (pieceToMove == null)
            {
                Debug.LogWarning($"기물({from})이 없는 곳에서 이동을 시도했습니다.");
                return false;
            }

            // TODO: 실제 체스 로직에서는 여기에 해당 기물의 GetPossibleMoves() 호출과
            // PieceMoveValidator를 통한 유효성 검사 로직이 포함되어야 합니다.
            // 여기서는 단순하게 이동만 처리합니다.

            PlacePiece(null, from); // 기존 위치를 비움
            PlacePiece(pieceToMove, to);   // 새 위치에 기물을 놓음
            Debug.Log($"기물 {pieceToMove.PieceName}가 {from}에서 {to}로 이동했습니다.");
            return true;
        }

        /// <summary>
        /// 보드에서 특정 기물을 제거합니다.
        /// </summary>
        /// <param name="pieceToRemove">제거할 기물 객체</param>
        public void RemovePiece(Piece pieceToRemove)
        {
            // 보드를 순회하며 해당 기물을 찾아 제거합니다.
            // 더 효율적인 방법으로 Dictionary<IPiece, Vector2> 같은 것을 사용해 기물 위치를 추적할 수도 있습니다.
            for (int x = 0; x < BoardSize.x; x++)
            {
                for (int y = 0; y < BoardSize.y; y++)
                {
                    if (_pieces[x, y] == pieceToRemove)
                    {
                        _pieces[x, y] = null; // 기물 제거
                        Debug.Log($"{pieceToRemove.PieceName}가 보드에서 제거되었습니다.");
                        return;
                    }
                }
            }
            Debug.LogWarning($"보드에서 {pieceToRemove.PieceName}를 찾을 수 없어 제거에 실패했습니다.");
        }
    }
}