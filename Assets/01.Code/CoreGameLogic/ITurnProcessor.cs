namespace Code.CoreGameLogic
{
    /// <summary>
    /// 한 턴의 모든 로직(예: 국정 턴, 기물 턴)을 실행하는 인터페이스
    /// </summary>
    public interface ITurnProcessor
    {
        /// <summary>
        /// 현재 턴 단계를 처리
        /// </summary>
        void ProcessTurn();

        void Initialize(GameManager gameManager);
    }
}