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
        }
    }
}