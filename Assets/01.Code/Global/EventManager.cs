using Code.CoreGameLogic;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Global
{
    public static class EventManager
    {
        public static UnityEvent<IPiece> OnPieceSelected = new();
        public static UnityEvent<Piece, Vector2> OnPieceMoved = new();
        public static UnityEvent OnTurnEnd = new();
        public static UnityEvent<string> OnLogMessage = new();
    }
}