using Code.Players;

namespace Code.CoreGameLogic
{
    /// <summary>
    /// 게임의 승리 조건을 확인하는 인터페이스
    /// </summary>
    public interface IWinConditionChecker
    {
        /// <summary>
        /// 특정 플레이어가 승리 조건을 만족했는지 확인
        /// </summary>
        /// <param name="player">확인할 플레이어 객체</param>
        /// <returns>승리 조건을 만족했으면 true, 아니면 false</returns>
        bool CheckForWin(PlayerID player);
    }
}