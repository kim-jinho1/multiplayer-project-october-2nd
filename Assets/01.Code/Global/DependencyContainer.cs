    using System;
    using System.Collections.Generic;
    using Code.CoreGameLogic;
    using Code.Players;
    using UnityEngine;

    namespace Code.Global
    {
        public static class DependencyContainer
        {
            private static readonly Dictionary<Type, object> Singletons = new();
            private static readonly Dictionary<string, object> NamedSingletons = new();

            public static void RegisterSingleton<TInterface, TConcrete>(TConcrete instance) where TConcrete : TInterface
            {
                if (Singletons.ContainsKey(typeof(TInterface)))
                {
                    Debug.LogWarning($"의존성 컨테이너: {typeof(TInterface).Name} (일반 싱글톤)이(가) 이미 등록되어 덮어씁니다.");
                }
                Singletons[typeof(TInterface)] = instance;
            }

            public static void RegisterNamedSingleton<TInterface, TConcrete>(Enum nameEnum, TConcrete instance) where TConcrete : TInterface
            {
                string name = nameEnum.ToString();
                if (NamedSingletons.ContainsKey(name))
                {
                    Debug.LogWarning($"의존성 컨테이너: 이름 '{name}' ({typeof(TInterface).Name})이(가) 이미 등록되어 덮어씁니다.");
                }
                NamedSingletons[name] = instance;
            }

            public static T Get<T>()
            {
                if (Singletons.TryGetValue(typeof(T), out object service))
                {
                    return (T)service;
                }

                throw new InvalidOperationException($"타입 {typeof(T).Name}에 대한 의존성이 등록되지 않았습니다.");
            }

            public static T Get<T>(Enum nameEnum)
            {
                string name = nameEnum.ToString();
                if (NamedSingletons.TryGetValue(name, out object service))
                {
                    return (T)service;
                }
                throw new InvalidOperationException($"이름이 지정된 의존성 '{name}' ({typeof(T).Name})이(가) 등록되지 않았습니다.");
            }

            private static void Clear()
            {
                Singletons.Clear();
                NamedSingletons.Clear();
            }
            
            public static void InitializeGameDependencies()
            {
                Clear();
                
                BattleResolver battleResolver = new BattleResolver();
                RegisterSingleton<IBattleResolver, BattleResolver>(battleResolver);

                WinConditionChecker winConditionChecker = new WinConditionChecker();
                RegisterSingleton<IWinConditionChecker, WinConditionChecker>(winConditionChecker);

                NationalTurnProcessor nationalTurnProcessor = UnityEngine.Object.FindAnyObjectByType<NationalTurnProcessor>();
                RegisterNamedSingleton<ITurnProcessor, NationalTurnProcessor>(TurnPhase.NationalTurn, nationalTurnProcessor);

                PieceTurnProcessor pieceTurnProcessor = UnityEngine.Object.FindAnyObjectByType<PieceTurnProcessor>();
                RegisterNamedSingleton<ITurnProcessor, PieceTurnProcessor>(TurnPhase.PieceTurn, pieceTurnProcessor);
            }
        }
    }
