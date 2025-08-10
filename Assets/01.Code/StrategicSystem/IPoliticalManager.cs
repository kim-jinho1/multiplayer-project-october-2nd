using Code.Global;
using Code.StrategicSystem;

namespace Code.StrategicSystem
{
    /// <summary>
    /// 정치 안정도와 정책 실행을 관리하는 기능을 정의하는 인터페이스
    /// </summary>
    public interface IPoliticalManager
    {
        /// <summary>
        /// 특정 정책을 실행
        /// </summary>
        void ExecutePolicy(IPolicy policy);

        /// <summary>
        /// 특정 정책 실행이 가능한지 확인
        /// </summary>
        bool CanExecutePolicy(IPolicy policy);
        
        void ApplyPolicy(PolicyData policy);
    }
}