namespace Code.StrategicSystem
{
    /// <summary>
    /// 게임 내 모든 정책이 가져야 할 공통 기능을 정의하는 인터페이스입니다.
    /// </summary>
    public interface IPolicy
    {
        /// <summary>
        /// 정책의 이름을 나타내는 속성입니다.
        /// </summary>
        string PolicyName { get; }

        /// <summary>
        /// 정책 실행에 필요한 비용을 나타내는 속성입니다.
        /// </summary>
        int Cost { get; }

        /// <summary>
        /// 정책의 실제 효과를 적용합니다.
        /// </summary>
        void Execute(IPlayerNationState state);
    }
}