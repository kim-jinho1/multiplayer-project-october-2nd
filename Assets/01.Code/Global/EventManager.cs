using Code.CoreGameLogic;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Global
{
    public static class EventManager
    {
        public static UnityEvent<IPiece> OnPieceSelected = new UnityEvent<IPiece>();
        public static UnityEvent<Piece, Vector2> OnPieceMoved = new UnityEvent<Piece, Vector2>();
        public static UnityEvent OnTurnEnd = new UnityEvent();
        public static UnityEvent<string> OnLogMessage = new UnityEvent<string>();
    }
}