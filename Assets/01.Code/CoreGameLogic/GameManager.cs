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
    /// 게임의 전체적인 흐름과 상태를 관리하는 중앙 컨트롤러
    /// 다른 시스템에 작업을 위임하는 지휘자 역할
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private IBoard _board;
        private ITurnProcessor _nationalTurnProcessor;
        private ITurnProcessor _pieceTurnProcessor;
        private IWinConditionChecker _winConditionChecker;
        private ICommandExecutor _commandExecutor;
        private IBattleResolver _battleResolver;
        

        private GameState _currentGameState;
        private TurnPhase _currentTurnPhase;
        private PlayerID _currentPlayerId;
        
        private Dictionary<PlayerID, Player> _players;

        private void Awake()
        {
            _board = DependencyContainer.Get<IBoard>();
            _nationalTurnProcessor = DependencyContainer.Get<ITurnProcessor>(TurnPhase.NationalTurn);
            _pieceTurnProcessor = DependencyContainer.Get<ITurnProcessor>(TurnPhase.PieceTurn);
            _winConditionChecker = DependencyContainer.Get<IWinConditionChecker>();
            _commandExecutor = DependencyContainer.Get<ICommandExecutor>();
            _battleResolver = DependencyContainer.Get<IBattleResolver>();
            var player1 = DependencyContainer.Get<Player>(PlayerID.Player1);
            var player2 = DependencyContainer.Get<Player>(PlayerID.Player2);
            _players = new Dictionary<PlayerID, Player>
            {
                { PlayerID.Player1, player1 },
                { PlayerID.Player2, player2 }
            };
            
            _currentGameState = GameState.Paused; // 게임 시작 전 상태
        }

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            _currentGameState = GameState.Playing;
            _currentPlayerId = PlayerID.Player1;
            _currentTurnPhase = TurnPhase.NationalTurn;
            
            Debug.Log("게임 시작! 현재 플레이어: " + _currentPlayerId + ", 현재 턴: " + _currentTurnPhase);
            ProcessCurrentTurn();
        }

        public void ProcessCurrentTurn()
        {
            if (_currentGameState != GameState.Playing) return;

            if (_currentTurnPhase == TurnPhase.NationalTurn)
                _nationalTurnProcessor.ProcessTurn();
            else // PieceTurn
                _pieceTurnProcessor.ProcessTurn();
        }
        
        /// <summary>
        /// 턴이 종료되면 호출되어 다음 턴으로 전환하는 로직을 수행
        /// </summary>
        public void EndCurrentTurn()
        {
            // PlayerID를 키로 사용하여 실제 Player 객체를 가져옴
            var currentPlayer = _players[_currentPlayerId];

            if (_winConditionChecker.CheckForWin(currentPlayer))
            {
                EndGame(currentPlayer);
                return;
            }
            
            _currentTurnPhase = (_currentTurnPhase == TurnPhase.NationalTurn) ? TurnPhase.PieceTurn : TurnPhase.NationalTurn;
            
            if (_currentTurnPhase == TurnPhase.NationalTurn)
                _currentPlayerId = (_currentPlayerId == PlayerID.Player1) ? PlayerID.Player2 : PlayerID.Player1;
            
            Debug.Log("다음 턴! 현재 플레이어: " + _currentPlayerId + ", 현재 턴: " + _currentTurnPhase);
            ProcessCurrentTurn();
        }

        /// <summary>
        /// 게임을 종료하고 승리자를 결정
        /// </summary>
        /// <param name="winner">게임에서 승리한 Player 객체</param>
        private void EndGame(Player winner)
        {
            _currentGameState = GameState.GameOver;
            Debug.Log($"{winner.ID}가 승리하여 게임이 종료되었습니다!");
            // 게임 종료 UI를 띄우는 로직을 추가
        }
        
        public void HandleBattle(Piece attacker, Piece defender)
        {
            _battleResolver.ResolveBattle(attacker, defender);
        }
    }
}