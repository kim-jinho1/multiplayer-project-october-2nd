using Code.CoreGameLogic;
using System.Collections.Generic;
using Code.StrategicSystem;
using UnityEngine;

namespace _01.Code.StrategicSystem
{
    public class LoyaltyManager : ILoyaltyManager
    {
        private readonly Dictionary<Piece, int> _loyaltyScores = new Dictionary<Piece, int>();
        
        // 특정 기물의 충성도를 반환합니다.
        public int GetLoyalty(Piece piece)
        {
            if (_loyaltyScores.TryGetValue(piece, out int score))
            {
                return score;
            }
            return 100; // 기본 충성도
        }

        // 특정 기물의 충성도를 변경합니다.
        public void ChangeLoyalty(Piece piece, int amount)
        {
            int currentLoyalty = GetLoyalty(piece);
            _loyaltyScores[piece] = Mathf.Clamp(currentLoyalty + amount, 0, 100);
            Debug.Log($"{piece.PieceName}의 충성도가 {amount}만큼 변경되어 현재 충성도는 {_loyaltyScores[piece]}입니다.");
        }

        // 모든 기물을 순회하며 반란 여부를 확인합니다.
        public void CheckForRebellion()
        {
            foreach (var entry in _loyaltyScores)
            {
                Piece piece = entry.Key;
                int loyalty = entry.Value;

                if (loyalty <= 10) // 충성도가 10 이하일 때 반란 발생
                {
                    Debug.Log($"{piece.PieceName}가 반란을 일으켜 보드에서 제거됩니다!");
                    
                    // 반란 로직: 기물을 보드에서 제거합니다.
                    // (실제 게임에서는 기물 소유주를 바꾸거나, 중립으로 만드는 등의 로직을 구현할 수 있습니다)
                    //_board.RemovePiece(piece);
                }
            }
        }
    }
}