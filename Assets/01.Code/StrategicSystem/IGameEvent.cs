namespace Code.StrategicSystem
{
    /// <summary>
    /// 게임 내에서 발생하는 다양한 이벤트의 공통 기능을 정의하는 인터페이스
    /// </summary>
    public interface IGameEvent
    {
        /// <summary>
        /// 이벤트의 제목을 나타내는 속성
        /// </summary>
        string EventTitle { get; }

        /// <summary>
        /// 이벤트의 설명을 나타내는 속성
        /// </summary>
        string EventDescription { get; }

        /// <summary>
        /// 이벤트의 효과를 발생
        /// </summary>
        void TriggerEvent(IPlayerNationState state);
    }
}