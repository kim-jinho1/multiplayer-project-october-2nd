namespace Code.StrategicSystem
{
    /// <summary>
    /// 플레이어(국가)의 현재 상태 데이터에 접근하는 기능을 정의하는 인터페이스
    /// </summary>
    public interface IPlayerNationState
    {
        /// <summary>
        /// 현재 보유한 골드 양을 나타내는 속성
        /// </summary>
        int Gold { get; }

        /// <summary>
        /// 현재 보유한 행동력(AP)을 나타내는 속성
        /// </summary>
        int ActionPoints { get; }

        /// <summary>
        /// 현재 기술 수준을 나타내는 속성
        /// </summary>
        int TechnologyLevel { get; }

        /// <summary>
        /// 현재 정치 안정도를 나타내는 속성
        /// </summary>
        int PoliticalStability { get; }
    }
}