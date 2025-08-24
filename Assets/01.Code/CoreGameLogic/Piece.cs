using System;
using System.Collections.Generic;
using Code.Global;
using PurrNet;
using UnityEngine;
using PlayerID = Code.Players.PlayerID;

namespace Code.CoreGameLogic
{
    public abstract class Piece : MonoBehaviour, IPiece , IMouseOver
    {
        [field:SerializeField] public PieceData PieceData { get; private set; }
        [field:SerializeField] public Material ChangeMaterial { get; private set; }
        public Material[] OriginalMaterials { get; private set; }
        public abstract string PieceName { get; }
        public SyncVar<PlayerID> OwnerID { get; private set;}
        public SyncVar<int> AttackPower { get; private set;}
        public SyncVar<int> DefensePower { get; private set;}
        public SyncVar<int> Health { get; private set;}
        public SyncVar<int> Loyalty { get; private set;}
        public SyncVar<int> ConsumptionAP { get; private set;}
        public SyncVar<int> ConsumptionGold { get; private set;}
        public SyncVar<bool> IsForward { get; private set;}
        public SyncVar<bool> IsAlive => new(Health > 0);
        
        protected readonly IPieceMoveValidator Validator;

        public Piece(IPieceMoveValidator validator)
        {
            Validator = validator;
        }

        public virtual void Awake()
        {
            // SyncVar 객체들을 Awake()에서 초기화합니다.
            
            OriginalMaterials = GetComponentInChildren<MeshRenderer>().materials;
        }

        private void OnEnable()
        {
            if(PieceData == null)
                return;
            
            Health = new SyncVar<int>();
            AttackPower = new SyncVar<int>();
            DefensePower = new SyncVar<int>();
            Loyalty = new SyncVar<int>();
            ConsumptionAP = new SyncVar<int>();
            ConsumptionGold = new SyncVar<int>();
            IsForward = new SyncVar<bool>();
            OwnerID = new SyncVar<PlayerID>();

            Health.value = PieceData.Health;
            AttackPower.value = PieceData.AttackPower;
            DefensePower.value = PieceData.DefensePower;
            Loyalty.value = PieceData.Loyalty;
            ConsumptionAP.value = PieceData.ConsumptionAP;
            ConsumptionGold.value = PieceData.ConsumptionGold;
            IsForward.value = PieceData.isForward;
            OwnerID.value = PieceData.OwnerID;
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
        
        public void ModifyAttackPower(int amount)
        {
            AttackPower.value += amount;
        }

        /// <summary>
        /// 기물이 움직일 수 있는 범위를 반환합니다.
        /// </summary>
        public abstract List<Vector3> GetMoveRange(Vector3 currentPos);
    }
}