using Code.Command;
using Code.StrategicSystem;
using UnityEngine;

namespace _01.Code.StrategicSystem.BoostPolicy
{
    public class TechBoostPolicy : ICommand
    {
        private readonly ITechnologyManager _techManager;
        private readonly TechnologyLevel _targetTech;
        private bool _isCompleted = false;

        public bool IsComplete => _isCompleted;

        public TechBoostPolicy(ITechnologyManager techManager, TechnologyLevel targetTech)
        {
            _techManager = techManager;
            _targetTech = targetTech;
        }

        public void Execute()
        {
            if (_techManager.CanResearch(_targetTech))
            {
                _techManager.Research(_targetTech);
                Debug.Log($"정책: 기술 {_targetTech} 연구가 시작되었습니다.");
                _isCompleted = true;
            }
            else
            {
                Debug.Log($"정책: 기술 {_targetTech} 연구를 진행할 수 없습니다.");
            }
        }

        public void Undo()
        {
            // 기술 연구는 되돌리기 복잡하므로, 일단 예시 로그만 출력
            Debug.Log($"정책 '기술 {_targetTech} 연구'를 되돌립니다.");
            _isCompleted = false;
        }
    }
}