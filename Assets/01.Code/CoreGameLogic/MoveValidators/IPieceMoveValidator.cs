

using UnityEngine;

namespace Code.CoreGameLogic
{
    /// <summary>
    /// 기물의 이동이 규칙에 맞는지 검증하는 인터페이스
    /// </summary>
    public interface IPieceMoveValidator
    {
        /// <summary>
        /// 특정 기물의 이동이 유효한 규칙인지 검증하고 결과를 반환
        /// </summary>
        /// <param name="piece">이동을 시도하는 Piece 객체</param>
        /// <param name="from">기물의 현재 위치 (Vector2)</param>
        /// <param name="to">기물이 이동할 목표 위치 (Vector2)</param>
        /// <returns>이동이 유효하면 true, 아니면 false를 반환</returns>
        bool IsValidMove(Piece piece, Vector3 from, Vector3 to);
    }
}