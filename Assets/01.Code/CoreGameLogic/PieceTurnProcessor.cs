using UnityEngine;

namespace Code.CoreGameLogic
{
    public class PieceTurnProcessor : ITurnProcessor
    {
        public void ProcessTurn()
        {
            Debug.Log("기물 턴을 처리합니다: 기물 이동, 공격 등...");
        }
    }
}