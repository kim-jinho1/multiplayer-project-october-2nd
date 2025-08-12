namespace Code.StrategicSystem
{
    public class PlayerNationState : IPlayerNationState
    {
        public int Gold { get; private set; } = 1000;
        public int ResearchPoints { get; private set; } = 0;
        public int ActionPoints { get; private set; } = 10;
        public int PoliticalStability { get; private set; } = 100;
        public TechnologyLevel CurrentTechLevel { get; private set; } = TechnologyLevel.Bronze;
        
        public void AddGold(int amount) => Gold += amount;
        public void AddResearchPoints(int amount) => ResearchPoints += amount;
        public void AddActionPoints(int amount) => ActionPoints += amount;
        public void ChangePoliticalStability(int amount)
        {
            PoliticalStability += amount;
            if (PoliticalStability > 100) PoliticalStability = 100;
            if (PoliticalStability < 0) PoliticalStability = 0;
        }

        public void SetTechnologyLevel(TechnologyLevel level) => CurrentTechLevel = level;
    }
}