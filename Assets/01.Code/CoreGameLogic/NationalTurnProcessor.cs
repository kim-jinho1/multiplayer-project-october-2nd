using UnityEngine;

namespace Code.CoreGameLogic
{
    public class NationalTurnProcessor : ITurnProcessor
    {
        public void ProcessTurn()
        {
            Debug.Log("국가 턴을 처리합니다: 자원 생산, 정책, 기술 등...");
        }
    }
}