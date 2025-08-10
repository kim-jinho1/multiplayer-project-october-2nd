using UnityEngine;

namespace Code.CoreGameLogic
{
    /// <summary>
    /// 게임 보드의 기본 기능을 정의하는 인터페이스
    /// </summary>
    public interface IBoard
    {
        /// <summary>
        /// 특정 위치에 있는 기물을 반환합니다. 해당 위치에 기물이 없으면 null을 반환
        /// </summary>
        /// <param name="position">확인할 보드 좌표 (Vector2).</param>
        /// <returns>해당 위치의 Piece 객체 또는 null</returns>
        Piece GetPieceAt(Vector2 position);

        /// <summary>
        /// 보드에 새로운 기물을 배치
        /// </summary>
        /// <param name="piece">배치할 Piece 객체</param>
        /// <param name="position">기물을 배치할 보드 좌표 (Vector2)</param>
        void PlacePiece(Piece piece, Vector2 position);

        /// <summary>
        /// 특정 위치의 기물을 다른 위치로 이동
        /// </summary>
        /// <param name="from">기물의 현재 위치 (Vector2)</param>
        /// <param name="to">기물이 이동할 목표 위치 (Vector2)</param>
        bool MovePiece(Vector2 from, Vector2 to);

        /// <summary>
        /// 보드의 크기(예: 8)를 나타내는 속성
        /// </summary>
        int BoardSize { get; }
        /// <summary>
        /// 기물 제거 메서드
        /// </summary>
        /// <param name="piece">제거할 기물</param>
        void RemovePiece(Piece piece);
    }
}