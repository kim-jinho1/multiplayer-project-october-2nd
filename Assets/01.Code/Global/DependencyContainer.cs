using System;
using System.Collections.Generic;

namespace Code.Global
{
    /// <summary>
    /// 게임 내 모든 의존성을 등록하고 해결(resolve)하는 정적 컨테이너 클래스입니다.
    /// 의존성 주입(Dependency Injection) 패턴을 구현합니다.
    /// </summary>
    public static class DependencyContainer
    {
        // 인터페이스 타입(key)과 그 구현체 인스턴스(value)를 저장하는 딕셔너리입니다.
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        /// <summary>
        /// 특정 인터페이스에 대한 구현체 인스턴스를 컨테이너에 등록합니다.
        /// </summary>
        /// <typeparam name="TInterface">등록할 인터페이스 타입.</typeparam>
        /// <param name="instance">해당 인터페이스를 구현한 인스턴스.</param>
        public static void Register<TInterface>(TInterface instance)
        {
            if (_services.ContainsKey(typeof(TInterface)))
            {
                // 이미 등록된 인터페이스인 경우 에러를 발생시킬 수 있습니다.
                // 또는 기존 인스턴스를 덮어쓰는 로직을 추가할 수 있습니다.
                return;
            }
            _services.Add(typeof(TInterface), instance);
        }

        /// <summary>
        /// 컨테이너에 등록된 인터페이스의 인스턴스를 가져옵니다.
        /// </summary>
        /// <typeparam name="TInterface">가져올 인터페이스 타입.</typeparam>
        /// <returns>등록된 인스턴스.</returns>
        public static TInterface Get<TInterface>()
        {
            if (_services.TryGetValue(typeof(TInterface), out object service))
            {
                return (TInterface)service;
            }

            throw new InvalidOperationException($"Service of type {typeof(TInterface).Name} not registered.");
        }

        /// <summary>
        /// (선택사항) 특정 키에 따라 다른 구현체를 등록하고 가져오는 오버로드입니다.
        /// 예를 들어, 여러 종류의 ITurnProcessor를 구분할 때 유용합니다.
        /// </summary>
        public static void Register<TInterface>(object key, TInterface instance)
        {
            // Dictionary<object, object>를 사용하여 복잡한 키를 처리할 수 있습니다.
            // 여기서는 단순함을 위해 예시로만 남겨둡니다.
        }

        public static TInterface Get<TInterface>(object key)
        {
            // Dictionary<object, object>를 사용하여 키에 맞는 인스턴스를 반환합니다.
            // 여기서는 단순함을 위해 예시로만 남겨둡니다.
            return Get<TInterface>();
        }
    }
}