using UnityEngine;

namespace Code.CoreGameLogic
{
    public class BattleResolver : IBattleResolver
    {
        public void ResolveBattle(Piece attacker, Piece defender)
        {
            int damage = attacker.AttackPower - defender.DefensePower;
            if (damage < 0) damage = 0;
            
            Debug.Log($"{attacker.PieceName} ({attacker.AttackPower})가 {defender.PieceName} ({defender.DefensePower})를 공격!");
            Debug.Log($"적용될 데미지: {damage}");
            
            defender.Health -= damage;
            Debug.Log($"{defender.PieceName}의 남은 체력: {defender.Health}");
            
            if (!defender.IsAlive)
            {
                Debug.Log($"{defender.PieceName}가 전투에서 패배하여 파괴되었습니다!");
            }
        }
    }
}