using Code.Command;

namespace Code.CoreGameLogic
{
    /// <summary>
    /// ICommand 객체를 받아서 실행하는 기능을 정의하는 인터페이스
    /// </summary>
    public interface ICommandExecutor
    {
        /// <summary>
        /// 주어진 ICommand 객체를 실행
        /// </summary>
        void ExecuteCommand(ICommand command);
    }
}