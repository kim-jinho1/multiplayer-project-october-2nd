namespace Code.StrategicSystem
{
    /// <summary>
    /// 기술 연구와 관련된 기능을 추상화하는 인터페이스
    /// </summary>
    public interface ITechnologyManager
    {
        /// <summary>
        /// 기술 연구를 진행
        /// </summary>
        void ResearchTechnology();

        /// <summary>
        /// 현재 기술 연구가 가능한지 확인
        /// </summary>
        bool CanResearch();

        /// <summary>
        /// 현재 기술 수준에 따른 효과를 적용
        /// </summary>
        void ApplyTechnologyEffects();
    }
}