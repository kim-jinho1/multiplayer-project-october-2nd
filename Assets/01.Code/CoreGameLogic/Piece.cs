using System.Collections.Generic;
using PurrNet;
using UnityEngine;
using PlayerID = Code.Players.PlayerID;

namespace Code.CoreGameLogic
{
    public abstract class Piece : MonoBehaviour, IPiece
    {
        public abstract string PieceName { get; }
        public SyncVar<PlayerID> OwnerID { get; private set; }
        public SyncVar<int> AttackPower { get; protected internal set; }
        public SyncVar<int> DefensePower { get; protected set; }
        public SyncVar<int> Health { get; protected internal set; }
        public SyncVar<bool> IsAlive => new(Health > 0);
        
        protected IPieceMoveValidator Validator;

        public Piece(PlayerID ownerId, IPieceMoveValidator validator)
        {
            OwnerID.value = ownerId;
            Validator = validator;
        }

        public bool IsMovePossible(IBoard board, Vector2 from, Vector2 to)
        {
            return Validator.IsValidMove(board, this, from, to);
        }

        public abstract SyncVar<List<Vector2>> GetPossibleMoves(IBoard board, Vector2 currentPos);
        
        public void ModifyAttackPower(int amount)
        {
            AttackPower.value += amount;
        }
    }
}