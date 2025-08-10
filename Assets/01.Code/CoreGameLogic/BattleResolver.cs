namespace Code.CoreGameLogic
{
    public class BattleResolver : IBattleResolver
    {
        public void ResolveBattle(Piece attacker, Piece defender)
        {
            // 1. 공격자와 방어자의 공격력/방어력 계산
            int damage = attacker.AttackPower - defender.DefensePower;
            if (damage < 0) damage = 0;
            
            // 2. 방어자의 체력을 감소시킵니다.
            defender.Health -= damage;
            
            // 3. 추가적인 전략 요소(기술, 정책 등)를 반영합니다.
            // 예시: defender.Health -= attacker.NationState.GetTechnologyEffect();
            
            // 4. 방어자가 죽었는지 확인합니다.
            if (!defender.IsAlive)
            {
                // 보드에서 기물을 제거하는 로직 등을 추가합니다.
            }
        }
    }
}