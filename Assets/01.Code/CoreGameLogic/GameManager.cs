    using System;
    using Code.Command;
    using Code.Global;
    using Code.Players;
    using UnityEngine;
    using System.Collections.Generic;
    using PurrNet;
    using PlayerID = Code.Players.PlayerID;
    using Random = System.Random;

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

            public string aaa;
            private bool isGameStarted = false;
            private IBoard _iBoard;
            private ITurnProcessor _nationalTurnProcessor;
            private ITurnProcessor _pieceTurnProcessor;
            private IWinConditionChecker _winConditionChecker;
            private IBattleResolver _battleResolver;
            
            private readonly SyncVar<GameState> _currentGameState = new();
            private readonly SyncVar<TurnPhase> _currentTurnPhase = new();
            public SyncVar<PlayerID> currentPlayerId = new();

            public SyncVar<Dictionary<PlayerID, PlayerData>> players;

            public IBoard IBoard => _iBoard;

            private void Awake()
            {
                _currentGameState.value = GameState.Paused;
                _currentTurnPhase.value = TurnPhase.NationalTurn;
                currentPlayerId.value = PlayerID.Player1;
            }

            protected override void OnSpawned()
            {
                base.OnSpawned();

                DependencyContainer.RegisterSingleton<GameManager, GameManager>(this);
                
                _iBoard = DependencyContainer.Get<IBoard>();
                _nationalTurnProcessor = DependencyContainer.Get<ITurnProcessor>(TurnPhase.NationalTurn);
                _pieceTurnProcessor = DependencyContainer.Get<ITurnProcessor>(TurnPhase.PieceTurn);
                _winConditionChecker = DependencyContainer.Get<IWinConditionChecker>();
                _battleResolver = DependencyContainer.Get<IBattleResolver>();
                
                _nationalTurnProcessor = DependencyContainer.Get<ITurnProcessor>(TurnPhase.NationalTurn);
                _nationalTurnProcessor.Initialize(this);
                    
                _pieceTurnProcessor = DependencyContainer.Get<ITurnProcessor>(TurnPhase.NationalTurn);
                _pieceTurnProcessor.Initialize(this);
                isGameStarted = true;
                Debug.Log("생성됨!");
            }

            private void Update()
            {
                if (players.value is { Count: > 1 } && !isGameStarted)
                {
                    isGameStarted = true;
                    StartGame();
                }
            }

            public PlayerData RegisterPlayer()
            {
                if (players.value == null)
                {
                    players.value = new Dictionary<PlayerID, PlayerData>();
                }

                PlayerData newPlayerData = null;

                if (networkManager.isHost)
                {
                    newPlayerData = new PlayerData(PlayerID.Player1, nameof(PlayerID.Player1), 100);
                    players.value.Add(PlayerID.Player1, newPlayerData);
                }
                else if (networkManager.isClient)
                {
                    newPlayerData = new PlayerData(PlayerID.Player2, nameof(PlayerID.Player2), 100);
                    players.value.Add(PlayerID.Player2, newPlayerData);
                }
                else
                    return null;

                return newPlayerData;
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
                    currentPlayerId.value = PlayerID.Player1;
                    _currentTurnPhase.value = TurnPhase.NationalTurn;
                    
                    ProcessCurrentTurn();
                }
            }
            
            /// <summary>
            /// 주어진 명령을 실행
            /// </summary>
            /// <param name="command">실행할 ICommand 객체</param>
            [ServerRpc]
            public void ExecuteCommand(ICommand command)
            {
                if (!networkManager.isServer)
                {
                    Debug.Log("ExecuteCommand: 클라이언트에서 명령을 PurrNet RPC를 통해 서버로 전송해야 합니다.");
                }
                else
                {
                    command.Execute();
                    Debug.Log("ExecuteCommand: 서버에서 명령을 직접 실행하고, 결과 동기화를 위한 ClientRpc를 호출합니다.");
                }
            }
            
            /// <summary>
            /// 현재 플레이어의 현재 턴 단계를 진행
            /// </summary>
            public void ProcessCurrentTurn()
            {
                if (_currentGameState.value != GameState.Playing) 
                    return;

                if (_currentTurnPhase.value == TurnPhase.NationalTurn)
                    _nationalTurnProcessor.ProcessTurn();
                else
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
                    PlayerData currentPlayerData = players.value[currentPlayerId.value]; 

                    if (_winConditionChecker.CheckForWin(currentPlayerData.ID)) 
                    { 
                        EndGame(currentPlayerData.ID); 
                        return; 
                    } 
                      
                    _currentTurnPhase.value = _currentTurnPhase.value == TurnPhase.NationalTurn ? TurnPhase.PieceTurn : TurnPhase.NationalTurn; 
                      
                    if (_currentTurnPhase.value == TurnPhase.NationalTurn)
                        currentPlayerId.value = currentPlayerId.value == PlayerID.Player1 ? PlayerID.Player2 : PlayerID.Player1; 
                      
                    Debug.Log("다음 턴! 현재 플레이어: " + currentPlayerId.value + ", 현재 턴: " + _currentTurnPhase.value);
                    ProcessCurrentTurn(); 
                } 
            } 

            /// <summary>
            /// 게임을 종료하고 승리자를 결정
            /// </summary>
            [ObserversRpc]
            private void EndGame(PlayerID winnerId)
            {
                PlayerData winner = players.value[winnerId]; 
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
