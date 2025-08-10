namespace Code.StrategicSystem
{
    public enum TechnologyLevel
    {
        None = 0,
        Bronze = 1,
        Iron = 2,
        Industrial = 3,
        Modern = 4,
    }

    public interface IPlayerNationState
    {
        // 자원 관련
        int Gold { get; }
        int ResearchPoints { get; }
        
        // 국가 상태 관련
        int ActionPoints { get; }
        int PoliticalStability { get; }
        
        // 기술 레벨 (int가 아닌 enum 타입으로 수정)
        TechnologyLevel CurrentTechLevel { get; }

        // 상태 변경 메서드
        void AddGold(int amount);
        void AddResearchPoints(int amount);
        void SetTechnologyLevel(TechnologyLevel level);
        void AddActionPoints(int amount);
        void ChangePoliticalStability(int amount);
    }
}