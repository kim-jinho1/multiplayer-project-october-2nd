using Code.Players;
using UnityEngine;

namespace Code.Global
{
    [CreateAssetMenu(fileName = "PieceData", menuName = "SO/PieceData", order = 0)]
    public class PieceData : ScriptableObject
    {
        public PlayerID OwnerID;
        public string PieceName;
        public int AttackPower;
        public int DefensePower;
        public int Health;
    }
}