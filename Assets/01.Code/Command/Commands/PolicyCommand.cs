using Code.Global;
using Code.StrategicSystem;
using UnityEngine;

namespace Code.Command.Commands
{
    public class PolicyCommand : ICommand
    {
        private readonly IPoliticalManager _politicalManager;
        private readonly PolicyData _policyData;
        private bool _isCompleted = false;
        
        public bool IsComplete => _isCompleted;

        public PolicyCommand(IPoliticalManager politicalManager, PolicyData policyData)
        {
            _politicalManager = politicalManager;
            _policyData = policyData;
        }

        public void Execute()
        {
            // 정책 실행 가능 여부는 별도의 로직으로 확인한다고 가정
            _politicalManager.ApplyPolicy(_policyData);
            _isCompleted = true;
            Debug.Log($"정책 명령 실행: '{_policyData.PolicyName}' 적용.");
        }

        public void Undo()
        {
            // 정책 효과를 되돌리는 로직 (자원 반환, 안정도 복구 등)
            Debug.Log($"정책 '{_policyData.PolicyName}' 적용을 되돌립니다.");
            _isCompleted = false;
        }
    }
}