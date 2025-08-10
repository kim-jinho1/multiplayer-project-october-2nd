namespace Code.StrategicSystem
{
    /// <summary>
    /// 기술 연구와 관련된 기능을 추상화하는 인터페이스입니다.
    /// </summary>
    public interface ITechnologyManager
    {
        /// <summary>
        /// 기술 연구를 진행합니다.
        /// </summary>
        void ResearchTechnology();

        /// <summary>
        /// 현재 기술 연구가 가능한지 확인합니다.
        /// </summary>
        bool CanResearch();

        /// <summary>
        /// 현재 기술 수준에 따른 효과를 적용합니다.
        /// </summary>
        void ApplyTechnologyEffects();
    }
}