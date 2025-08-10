using Code.Global;
using UnityEngine;

namespace Code.StrategicSystem
{
    public class PoliticalManager : IPoliticalManager
    {
        private readonly IPlayerNationState _nationState;

        public PoliticalManager(IPlayerNationState nationState)
        {
            _nationState = nationState;
        }

        public void ApplyPolicy(PolicyData policy)
        {
            _nationState.AddGold(policy.GoldBonus);
            _nationState.ChangePoliticalStability(policy.StabilityChange);
            
            Debug.Log($"정책 '{policy.PolicyName}' 적용. 골드 {_nationState.Gold}, 정치 안정도 {_nationState.PoliticalStability}");
        }

        // IPolicy 객체를 실행하는 메서드
        public void ExecutePolicy(IPolicy policy)
        {
            // 실제 정책 실행 로직을 IPolicy 객체에 위임
            if (CanExecutePolicy(policy))
            {
                policy.Execute();
                _nationState.AddActionPoints(-5); // 정책 실행 비용
                Debug.Log("정책이 실행되었습니다.");
            }
        }
        
        // IPolicy 객체 실행 가능 여부를 확인하는 메서드
        public bool CanExecutePolicy(IPolicy policy)
        {
            // ActionPoints가 충분한지 확인하는 로직
            if (_nationState.ActionPoints >= 5)
            {
                return true;
            }
            Debug.Log("행동력이 부족하여 정책을 실행할 수 없습니다.");
            return false;
        }
    }
}