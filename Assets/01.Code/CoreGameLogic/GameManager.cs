using Code.Command;
using Code.Global;
using Code.Players;
using UnityEngine;
using System.Collections.Generic;

namespace Code.CoreGameLogic
{
    /// <summary>
    /// 게임 스테이트
    /// </summary>
    public enum GameState
    {
        Playing, Paused, GameOver
    }

    /// <summary>
    /// 게임 턴
    /// </summary>
    public enum TurnPhase
    {
        NationalTurn, PieceTurn
    }

    /// <summary>
    /// 게임의 전체적인 흐름과 상태를 관리하는 중앙 컨트롤러입니다.
    /// 다른 시스템에 작업을 위임하는 지휘자 역할을 수행합니다.
    /// </summary>
    public class GameManager : MonoBehaviour, ICommandExecutor // ICommandExecutor를 구현하도록 추가
    {
        private IBoard _board;
        private ITurnProcessor _nationalTurnProcessor;
        private ITurnProcessor _pieceTurnProcessor;
        private IWinConditionChecker _winConditionChecker;
        // 이제 GameManager 자체가 ICommandExecutor이므로 이 필드는 필요 없습니다.
        // private ICommandExecutor _commandExecutor;
        private IBattleResolver _battleResolver;


        private GameState _currentGameState;
        private TurnPhase _currentTurnPhase;
        private PlayerID _currentPlayerId;

        private Dictionary<PlayerID, Player> _players;

        // Board 프로퍼티: _board 필드를 외부에서 접근할 수 있도록 합니다.
        public IBoard Board => _board;

        private void Awake()
        {
            // DependencyContainer가 이 GameManager의 Awake()보다 먼저 초기화되었는지 확인해야 합니다.
            // 보통 별도의 초기화 스크립트에서 DependencyContainer.InitializeGameDependencies()를 호출합니다.

            _board = DependencyContainer.Get<IBoard>();
            _nationalTurnProcessor = DependencyContainer.Get<ITurnProcessor>(TurnPhase.NationalTurn);
            _pieceTurnProcessor = DependencyContainer.Get<ITurnProcessor>(TurnPhase.PieceTurn);
            _winConditionChecker = DependencyContainer.Get<IWinConditionChecker>();
            // GameManager가 ICommandExecutor를 구현하므로, 자기 자신을 사용합니다.
            // _commandExecutor = this; // 이렇게 명시적으로 할당할 필요는 없지만, 개념적으로 그렇습니다.
            _battleResolver = DependencyContainer.Get<IBattleResolver>();

            // PlayerID를 사용하여 플레이어 인스턴스를 가져옵니다.
            var player1 = DependencyContainer.Get<Player>(PlayerID.Player1);
            var player2 = DependencyContainer.Get<Player>(PlayerID.Player2);
            _players = new Dictionary<PlayerID, Player>
            {
                { PlayerID.Player1, player1 },
                { PlayerID.Player2, player2 }
            };

            _currentGameState = GameState.Paused; // 게임 시작 전 초기 상태
        }

        private void Start()
        {
            StartGame();
        }

        /// <summary>
        /// 게임을 시작합니다.
        /// </summary>
        public void StartGame()
        {
            _currentGameState = GameState.Playing;
            _currentPlayerId = PlayerID.Player1; // 첫 턴은 Player1
            _currentTurnPhase = TurnPhase.NationalTurn; // 첫 턴 단계는 NationalTurn
            
            Debug.Log("게임 시작! 현재 플레이어: " + _currentPlayerId + ", 현재 턴: " + _currentTurnPhase);
            ProcessCurrentTurn();
        }
        
        /// <summary>
        /// ICommandExecutor 인터페이스의 메서드 구현입니다.
        /// 주어진 명령(Command)을 실행합니다.
        /// </summary>
        /// <param name="command">실행할 ICommand 객체</param>
        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            // 필요하다면 여기에 명령 스택에 푸시하는 로직 등을 추가하여 Undo/Redo 시스템을 구현할 수 있습니다.
        }

        /// <summary>
        /// 현재 플레이어의 현재 턴 단계를 진행합니다.
        /// </summary>
        public void ProcessCurrentTurn()
        {
            if (_currentGameState != GameState.Playing) return;

            if (_currentTurnPhase == TurnPhase.NationalTurn)
                _nationalTurnProcessor.ProcessTurn();
            else // PieceTurn
                _pieceTurnProcessor.ProcessTurn();
        }
        
        /// <summary>
        /// 현재 턴이 종료되면 호출되어 다음 턴으로 전환하는 로직을 수행합니다.
        /// </summary>
        public void EndCurrentTurn()
        {
            // 현재 플레이어 객체를 가져옵니다.
            var currentPlayer = _players[_currentPlayerId];

            // 승리 조건을 확인합니다.
            if (_winConditionChecker.CheckForWin(currentPlayer))
            {
                EndGame(currentPlayer);
                return;
            }
            
            // 턴 단계를 전환합니다 (NationalTurn <-> PieceTurn).
            _currentTurnPhase = (_currentTurnPhase == TurnPhase.NationalTurn) ? TurnPhase.PieceTurn : TurnPhase.NationalTurn;
            
            // NationalTurn 단계로 돌아오면 플레이어를 전환합니다.
            if (_currentTurnPhase == TurnPhase.NationalTurn)
            {
                _currentPlayerId = (_currentPlayerId == PlayerID.Player1) ? PlayerID.Player2 : PlayerID.Player1;
            }
            
            Debug.Log("다음 턴! 현재 플레이어: " + _currentPlayerId + ", 현재 턴: " + _currentTurnPhase);
            ProcessCurrentTurn(); // 다음 턴 처리 시작
        }

        /// <summary>
        /// 게임을 종료하고 승리자를 결정합니다.
        /// </summary>
        /// <param name="winner">게임에서 승리한 Player 객체</param>
        private void EndGame(Player winner)
        {
            _currentGameState = GameState.GameOver;
            Debug.Log($"{winner.ID}가 승리하여 게임이 종료되었습니다!");
            // 게임 종료 UI를 띄우는 로직이나 게임 재시작 옵션 등을 추가할 수 있습니다.
        }
        
        /// <summary>
        /// 두 기물 간의 전투를 처리합니다.
        /// </summary>
        /// <param name="attacker">공격하는 기물</param>
        /// <param name="defender">방어하는 기물</param>
        public void HandleBattle(Piece attacker, Piece defender)
        {
            _battleResolver.ResolveBattle(attacker, defender);
        }
    }
}