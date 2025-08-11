using Code.Command;
using Code.Global;
using Code.Players;
using UnityEngine;
using System.Collections.Generic;
using PurrNet;
using PlayerID = Code.Players.PlayerID;

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
    /// 다른 시스템에 작업을 위임하는 지휘자 역할을 수행
    /// </summary>
    public class GameManager : NetworkBehaviour, ICommandExecutor
    {
        [SerializeField] private new NetworkManager networkManager;
        
        private IBoard _iBoard;
        private ITurnProcessor _nationalTurnProcessor;
        private ITurnProcessor _pieceTurnProcessor;
        private IWinConditionChecker _winConditionChecker;
        private IBattleResolver _battleResolver;
        
        private SyncVar<GameState> _currentGameState = new();
        private SyncVar<TurnPhase> _currentTurnPhase = new();
        private SyncVar<PlayerID> _currentPlayerId = new();

        private Dictionary<PlayerID, Player> _players;

        public IBoard IBoard => _iBoard;

        private void Awake()
        {
            _currentGameState.value = GameState.Paused;
            _currentTurnPhase.value = TurnPhase.NationalTurn;
            _currentPlayerId.value = PlayerID.Player1;
        }

        protected override void OnSpawned()
        {
            base.OnSpawned();
            
            _iBoard = DependencyContainer.Get<IBoard>();
            _nationalTurnProcessor = DependencyContainer.Get<ITurnProcessor>(TurnPhase.NationalTurn);
            _pieceTurnProcessor = DependencyContainer.Get<ITurnProcessor>(TurnPhase.PieceTurn);
            _winConditionChecker = DependencyContainer.Get<IWinConditionChecker>();
            _battleResolver = DependencyContainer.Get<IBattleResolver>();
            
            Player player1 = DependencyContainer.Get<Player>(PlayerID.Player1);
            Player player2 = DependencyContainer.Get<Player>(PlayerID.Player2);
            _players = new Dictionary<PlayerID, Player>
            {
                { PlayerID.Player1, player1 },
                { PlayerID.Player2, player2 }
            };
        }

        protected override void OnDespawned()
        {
            base.OnDespawned();
        }
        
        private void OnGameStateChanged(GameState oldState, GameState newState)
        {
            Debug.Log($"게임 상태 변경 (네트워크 동기화): {oldState} -> {newState}");
        }
        
        private void OnTurnPhaseChanged(TurnPhase oldPhase, TurnPhase newPhase)
        {
            Debug.Log($"턴 단계 변경 (네트워크 동기화): {oldPhase} -> {newPhase}");
        }
        
        private void OnPlayerIdChanged(PlayerID oldId, PlayerID newId)
        {
            Debug.Log($"현재 플레이어 변경 (네트워크 동기화): {oldId} -> {newId}");
        }

        /// <summary>
        /// 게임을 시작합니다
        /// </summary>
        [ServerRpc]
        public void StartGame()
        {
            if (networkManager.isServer)
            {
                _currentGameState.value = GameState.Playing;
                _currentPlayerId.value = PlayerID.Player1;
                _currentTurnPhase.value = TurnPhase.NationalTurn;

                Debug.Log("게임 시작! 현재 플레이어: " + _currentPlayerId + ", 현재 턴: " + _currentTurnPhase);
                ProcessCurrentTurn();
            }
        }
        
        /// <summary>
        /// ICommandExecutor 인터페이스의 메서드 구현
        /// 주어진 명령(Command)을 실행
        /// </summary>
        /// <param name="command">실행할 ICommand 객체</param>
        public void ExecuteCommand(ICommand command)
        {
            Debug.Log("ExecuteCommand: 명령을 PurrNet RPC를 통해 서버로 전송해야 합니다.");
        }
        
        /// <summary>
        /// 현재 플레이어의 현재 턴 단계를 진행
        /// </summary>
        public void ProcessCurrentTurn()
        {
            if (_currentGameState.value != GameState.Playing) return;

            if (_currentTurnPhase.value == TurnPhase.NationalTurn)
                _nationalTurnProcessor.ProcessTurn();
            else // PieceTurn
                _pieceTurnProcessor.ProcessTurn();
        }
        
        /// <summary>
        /// 현재 턴이 종료되면 호출되어 다음 턴으로 전환하는 로직을 수행
        /// </summary>
        [ServerRpc]
        public void EndCurrentTurn()
        {
            if (networkManager.isServer)
            {
                var currentPlayer = _players[_currentPlayerId];

                if (_winConditionChecker.CheckForWin(currentPlayer))
                {
                    EndGame(currentPlayer);
                    return;
                }
                
                _currentTurnPhase.value = _currentTurnPhase.value == TurnPhase.NationalTurn ? TurnPhase.PieceTurn : TurnPhase.NationalTurn;
                
                if (_currentTurnPhase == TurnPhase.NationalTurn)
                {
                    _currentPlayerId.value = _currentPlayerId.value == PlayerID.Player1 ? PlayerID.Player2 : PlayerID.Player1;
                }
                
                Debug.Log("다음 턴! 현재 플레이어: " + _currentPlayerId + ", 현재 턴: " + _currentTurnPhase);
                ProcessCurrentTurn();
            }
        }

        /// <summary>
        /// 게임을 종료하고 승리자를 결정
        /// </summary>
        [ObserversRpc]
        private void EndGame(Player winner)
        {
            if (networkManager.isServer)
            {
                _currentGameState.value = GameState.GameOver;
                Debug.Log($"{winner.ID}가 승리하여 게임이 종료되었습니다!");
            }
        }
        
        /// <summary>
        /// 두 기물 간의 전투를 처리
        /// </summary>
        [ServerRpc]
        public void HandleBattle(Piece attacker, Piece defender)
        {
            if (networkManager.isServer)
            {
                _battleResolver.ResolveBattle(attacker, defender);
            }
        }
    }
}
