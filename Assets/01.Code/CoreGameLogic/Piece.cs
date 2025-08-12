using System.Collections.Generic;
using Code.Global;
using PurrNet;
using UnityEngine;
using PlayerID = Code.Players.PlayerID;

namespace Code.CoreGameLogic
{
    public abstract class Piece : MonoBehaviour, IPiece
    {
        [field:SerializeField] public PieceData pieceData { get; private set; }
        public abstract string PieceName { get; }
        public SyncVar<PlayerID> OwnerID { get; }
        public SyncVar<int> AttackPower { get; }
        public SyncVar<int> DefensePower { get; }
        public SyncVar<int> Health { get; }
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