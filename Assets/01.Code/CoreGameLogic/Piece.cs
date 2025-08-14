using System;
using System.Collections.Generic;
using System.Linq;
using Code.Global;
using PurrNet;
using Unity.VisualScripting;
using UnityEngine;
using PlayerID = Code.Players.PlayerID;

namespace Code.CoreGameLogic
{
    public abstract class Piece : MonoBehaviour, IPiece , IMouseOver
    {
        [field:SerializeField] public PieceData PieceData { get; private set; }
        [field:SerializeField] public Material ChangeMaterial { get; private set; }
         public Material[] OriginalMaterials { get; set; }
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

        public virtual void Awake()
        {
            OriginalMaterials = GetComponentInChildren<MeshRenderer>().materials;
        }

        private void OnMouseEnter()
        {
            MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
            Material[] newMaterials = new Material[OriginalMaterials.Length + 1];
            Array.Copy(OriginalMaterials, newMaterials, OriginalMaterials.Length);
            newMaterials[^1] = ChangeMaterial;
            mr.materials = newMaterials;
        }

        private void OnMouseExit()
        {
            MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
            mr.materials = OriginalMaterials;
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