using Code.Command;

namespace Code.CoreGameLogic
{
    /// <summary>
    /// ICommand 객체를 받아서 실행하는 기능을 정의하는 인터페이스
    /// </summary>
    public interface ICommandExecutor
    {
        /// <summary>
        /// 주어진 명령(Command)을 실행합니다.
        /// </summary>
        /// <param name="command">실행할 ICommand 객체</param>
        void ExecuteCommand(ICommand command);
    }
}