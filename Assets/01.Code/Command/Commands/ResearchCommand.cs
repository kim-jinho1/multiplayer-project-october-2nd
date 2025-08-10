using Code.StrategicSystem;
using UnityEngine;

namespace Code.Command.Commands
{
    public class ResearchCommand : ICommand
    {
        private readonly ITechnologyManager _techManager;
        private readonly TechnologyLevel _techToResearch;
        private bool _isCompleted = false;
        
        public bool IsComplete => _isCompleted;

        public ResearchCommand(ITechnologyManager techManager, TechnologyLevel techToResearch)
        {
            _techManager = techManager;
            _techToResearch = techToResearch;
        }

        public void Execute()
        {
            if (_techManager.CanResearch(_techToResearch))
            {
                _techManager.Research(_techToResearch);
                _isCompleted = true;
                Debug.Log($"연구 명령 실행: 기술 {_techToResearch} 연구 시작.");
            }
            else
            {
                Debug.Log($"연구 명령 실패: 기술 {_techToResearch} 연구를 진행할 수 없습니다.");
            }
        }

        public void Undo()
        {
            // 기술 연구는 되돌리기 복잡하므로, 일단 로그만 남깁니다.
            Debug.Log("연구 명령은 되돌릴 수 없습니다.");
        }
    }
}