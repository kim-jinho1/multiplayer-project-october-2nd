namespace Code.CoreGameLogic
{
    /// <summary>
    /// 한 턴의 모든 로직(예: 국정 턴, 기물 턴)을 실행하는 인터페이스입니다.
    /// </summary>
    public interface ITurnProcessor
    {
        /// <summary>
        /// 한 턴의 시작부터 끝까지 필요한 모든 처리를 순차적으로 실행합니다.
        /// </summary>
        void ProcessTurn();
    }
}