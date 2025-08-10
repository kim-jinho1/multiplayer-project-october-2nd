namespace Code.CoreGameLogic
{
    /// <summary>
    /// 기물 간의 전투를 처리하는 인터페이스
    /// </summary>
    public interface IBattleResolver
    {
        /// <summary>
        /// 공격자와 방어자의 전투를 처리하고, 피해량과 승패 결과를 적용
        /// </summary>
        void ResolveBattle(Piece attacker, Piece defender);
    }
}