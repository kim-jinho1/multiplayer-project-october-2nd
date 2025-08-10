using Code.Command;

namespace Code.CoreGameLogic
{
    public class CommandProcessor : ICommandExecutor
    {
        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }
    }
}