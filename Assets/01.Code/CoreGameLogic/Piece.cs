using System.Collections.Generic;
using Code.Players;
using UnityEngine;

namespace Code.CoreGameLogic
{
    public abstract class Piece : IPiece
    {
        public abstract string PieceName { get; }
        public PlayerID OwnerID { get; private set; }
        public int AttackPower { get; protected internal set; }
        public int DefensePower { get; protected set; }
        public int Health { get; protected internal set; }
        public bool IsAlive => Health > 0;
        
        protected IPieceMoveValidator _validator;

        public Piece(PlayerID ownerId, IPieceMoveValidator validator)
        {
            OwnerID = ownerId;
            _validator = validator;
        }

        public bool IsMovePossible(IBoard board, Vector2 from, Vector2 to)
        {
            return _validator.IsValidMove(board, this, from, to);
        }

        public abstract List<Vector2> GetPossibleMoves(IBoard board, Vector2 currentPos);
        
        public void ModifyAttackPower(int amount)
        {
            AttackPower += amount;
        }
    }
}