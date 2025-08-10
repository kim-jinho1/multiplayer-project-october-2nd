using Code.Players;

namespace Code.CoreGameLogic
{
    /// <summary>
    /// 게임의 승리 조건을 확인하는 인터페이스입니다.
    /// </summary>
    public interface IWinConditionChecker
    {
        /// <summary>
        /// 특정 플레이어가 게임에서 승리했는지 확인하고 결과를 반환합니다.
        /// </summary>
        bool CheckForWin(Player player);
    }
}