namespace Code.CoreGameLogic
{
    /// <summary>
    /// 기물 간의 전투를 처리하는 인터페이스
    /// </summary>
    public interface IBattleResolver
    {
        /// <summary>
        /// 두 기물 간의 전투를 해결하고 결과를 적용합니다.
        /// </summary>
        /// <param name="attacker">공격하는 기물</param>
        /// <param name="defender">방어하는 기물</param>
        void ResolveBattle(Piece attacker, Piece defender);
    }
}