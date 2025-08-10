using System.Collections.Generic;
using UnityEngine;

namespace Code.CoreGameLogic
{
    /// <summary>
    /// 모든 체스 기물이 가져야 할 공통 기능과 속성을 정의하는 인터페이스
    /// </summary>
    public interface IPiece
    {
        /// <summary>
        /// 기물의 이름을 나타내는 속성
        /// </summary>
        string PieceName { get; }

        /// <summary>
        /// 기물의 공격력을 나타내는 속성
        /// </summary>
        int AttackPower { get; }

        /// <summary>
        /// 기물의 방어력을 나타내는 속성
        /// </summary>
        int DefensePower { get; }

        /// <summary>
        /// 기물의 현재 체력을 나타내는 속성
        /// </summary>
        int Health { get; }

        /// <summary>
        /// 기물의 생존 여부를 나타내는 속성
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// 현재 보드 상태에서 기물이 이동 가능한 모든 위치를 계산하여 반환
        /// </summary>
        /// <param name="board">현재 게임 보드 상태를 나타내는 IBoard 객체</param>
        /// <returns>이동 가능한 위치 목록 (List<Vector2>) </returns>
        List<Vector2> GetPossibleMoves(IBoard board);
    }
}