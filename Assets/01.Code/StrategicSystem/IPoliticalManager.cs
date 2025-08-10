using Code.StrategicSystem;

namespace Code.StrategicSystem
{
    /// <summary>
    /// 정치 안정도와 정책 실행을 관리하는 기능을 정의하는 인터페이스입니다.
    /// </summary>
    public interface IPoliticalManager
    {
        /// <summary>
        /// 특정 정책을 실행합니다.
        /// </summary>
        void ExecutePolicy(IPolicy policy);

        /// <summary>
        /// 특정 정책 실행이 가능한지 확인합니다.
        /// </summary>
        bool CanExecutePolicy(IPolicy policy);
    }
}