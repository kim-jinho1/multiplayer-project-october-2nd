using Code.CoreGameLogic;
using UnityEngine;
using Code.Global;

namespace GameInitialization
{
    /// <summary>
    /// 게임 시작 시 필요한 모든 초기화 및 의존성 등록을 수행하는 스크립트
    /// </summary>
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        
        private void Awake()
        {
            DependencyContainer.InitializeGameDependencies();
            Debug.Log("DependencyContainer 초기화 완료.");
            
            if (gameManager != null)
            {
                GameObject gameManagerGO = new GameObject("GameManager");
                gameManagerGO.AddComponent<GameManager>();
                Debug.Log("GameManager GameObject 생성 및 컴포넌트 추가 완료.");
            }
        }
    }
}