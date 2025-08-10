using UnityEngine;

namespace Code.StrategicSystem
{
    public class TechnologyManager : ITechnologyManager
    {
        private readonly IPlayerNationState _nationState;

        public TechnologyManager(IPlayerNationState nationState)
        {
            _nationState = nationState;
        }

        public bool CanResearch(TechnologyLevel tech)
        {
            return _nationState.ResearchPoints >= GetTechCost(tech) && _nationState.CurrentTechLevel < tech;
        }

        public void Research(TechnologyLevel tech)
        {
            if (CanResearch(tech))
            {
                _nationState.AddResearchPoints(-GetTechCost(tech));
                _nationState.SetTechnologyLevel(tech);
                Debug.Log($"기술 {tech} 연구 완료!");
                
                // 기술 효과를 적용하는 메서드 호출
                ApplyTechnologyEffects();
            }
        }
        
        // 인터페이스의 멤버를 구현
        public void ApplyTechnologyEffects()
        {
            // 예시: 기술 레벨에 따라 플레이어의 공격력에 보너스를 주는 로직
            Debug.Log($"기술 레벨 {_nationState.CurrentTechLevel}에 따른 효과를 적용합니다.");
        }

        private int GetTechCost(TechnologyLevel tech) => (int)tech * 100;
    }
}