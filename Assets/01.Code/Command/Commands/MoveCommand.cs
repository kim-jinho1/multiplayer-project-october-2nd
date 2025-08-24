using Code.CoreGameLogic;
using UnityEngine;

namespace Code.Command.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly Vector2 _from;
        private readonly Vector2 _to;
        private bool _isCompleted = false;

        public bool IsComplete => _isCompleted;
        
        private readonly Piece _piece;
        private readonly Vector3 _destination;

        public MoveCommand(Piece piece, Vector3 destination)
        {
            _piece = piece;
            _destination = destination;
        }

        public void Execute()
        {
            
        }

        public void Undo()
        {
           
        }
    }
}