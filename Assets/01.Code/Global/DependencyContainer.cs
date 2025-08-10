using System;
using System.Collections.Generic;
using Code.CoreGameLogic;
using Code.Players;
using Code.StrategicSystem;
using Code.Command;
using UnityEngine;

namespace Code.Global
{
    public static class DependencyContainer
    {
        private static readonly Dictionary<Type, object> _singletons = new Dictionary<Type, object>();
        private static readonly Dictionary<string, object> _namedSingletons = new Dictionary<string, object>();

        private static void RegisterSingleton<TInterface, TConcrete>(TConcrete instance) where TConcrete : TInterface
        {
            if (_singletons.ContainsKey(typeof(TInterface)))
            {
                Debug.LogWarning($"의존성 컨테이너: {typeof(TInterface).Name} (일반 싱글톤)이(가) 이미 등록되어 덮어씁니다.");
            }
            _singletons[typeof(TInterface)] = instance;
        }

        private static void RegisterNamedSingleton<TInterface, TConcrete>(Enum nameEnum, TConcrete instance) where TConcrete : TInterface
        {
            string name = nameEnum.ToString();
            if (_namedSingletons.ContainsKey(name))
            {
                Debug.LogWarning($"의존성 컨테이너: 이름 '{name}' ({typeof(TInterface).Name})이(가) 이미 등록되어 덮어씁니다.");
            }
            _namedSingletons[name] = instance;
        }

        public static T Get<T>()
        {
            if (_singletons.TryGetValue(typeof(T), out object service))
            {
                return (T)service;
            }

            throw new InvalidOperationException($"타입 {typeof(T).Name}에 대한 의존성이 등록되지 않았습니다.");
        }

        public static T Get<T>(Enum nameEnum)
        {
            string name = nameEnum.ToString();
            if (_namedSingletons.TryGetValue(name, out object service))
            {
                return (T)service;
            }
            throw new InvalidOperationException($"이름이 지정된 의존성 '{name}' ({typeof(T).Name})이(가) 등록되지 않았습니다.");
        }

        private static void Clear()
        {
            _singletons.Clear();
            _namedSingletons.Clear();
            Debug.Log("의존성 컨테이너가 초기화되었습니다.");
        }

        public static void InitializeGameDependencies()
        {
            Clear();

            var board = new ChessBoard();
            RegisterSingleton<IBoard, ChessBoard>(board);

            var battleResolver = new BattleResolver();
            RegisterSingleton<IBattleResolver, BattleResolver>(battleResolver);

            var winConditionChecker = new WinConditionChecker();
            RegisterSingleton<IWinConditionChecker, WinConditionChecker>(winConditionChecker);

            var nationalTurnProcessor = new NationalTurnProcessor();
            RegisterNamedSingleton<ITurnProcessor, NationalTurnProcessor>(TurnPhase.NationalTurn, nationalTurnProcessor);

            var pieceTurnProcessor = new PieceTurnProcessor();
            RegisterNamedSingleton<ITurnProcessor, PieceTurnProcessor>(TurnPhase.PieceTurn, pieceTurnProcessor);
            
            var player1 = new Player(PlayerID.Player1);
            RegisterNamedSingleton<Player, Player>(PlayerID.Player1, player1);
            var player2 = new Player(PlayerID.Player2);
            RegisterNamedSingleton<Player, Player>(PlayerID.Player2, player2);
        }
    }
}
