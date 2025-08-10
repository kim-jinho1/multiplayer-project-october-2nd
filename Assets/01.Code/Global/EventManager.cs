using Code.CoreGameLogic;
using UnityEngine;
using UnityEngine.Events;

namespace Code.StrategicSystem
{
    public static class EventManager
    {
        public static UnityEvent<Piece> OnPieceSelected = new UnityEvent<Piece>();
        public static UnityEvent<Piece, Vector2> OnPieceMoved = new UnityEvent<Piece, Vector2>();
        public static UnityEvent OnTurnEnd = new UnityEvent();
        public static UnityEvent<string> OnLogMessage = new UnityEvent<string>();
    }
}