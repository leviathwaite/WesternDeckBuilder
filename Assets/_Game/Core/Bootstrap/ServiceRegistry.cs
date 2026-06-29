using System;
using System.Collections.Generic;
using UnityEngine;

namespace WesternDeckBuilder.Core
{
    /// <summary>
    /// Central service locator. Services register themselves during bootstrap;
    /// consumers resolve by interface type at runtime with no inspector wiring required.
    /// <para>
    /// Typical usage:
    /// <code>
    /// // Registration (GameBootstrap)
    /// ServiceRegistry.Instance.Register&lt;IHeatManager&gt;(new HeatManager());
    ///
    /// // Resolution (any runtime code)
    /// var heat = ServiceRegistry.Instance.Resolve&lt;IHeatManager&gt;();
    /// </code>
    /// Prefer <see cref="Services"/> for frequent access.
    /// </para>
    /// </summary>
    public sealed class ServiceRegistry
    {
        private static ServiceRegistry _instance;

        /// <summary>The singleton instance. Created on first access.</summary>
        public static ServiceRegistry Instance => _instance ?? (_instance = new ServiceRegistry());

        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        private ServiceRegistry() { }

        /// <summary>
        /// Registers a service under its interface type.
        /// Overwrites any previously registered service of the same type and logs a warning.
        /// </summary>
        public void Register<T>(T service) where T : class
        {
            if (service == null)
            {
                Debug.LogError($"[ServiceRegistry] Attempted to register null for {typeof(T).Name}.");
                return;
            }
            var type = typeof(T);
            if (_services.ContainsKey(type))
                Debug.LogWarning($"[ServiceRegistry] Overwriting existing service: {type.Name}.");
            _services[type] = service;
        }

        /// <summary>
        /// Resolves a registered service by interface type.
        /// Logs an error and returns <c>null</c> if the service was not registered.
        /// </summary>
        public T Resolve<T>() where T : class
        {
            if (_services.TryGetValue(typeof(T), out var service))
                return (T)service;

            Debug.LogError(
                $"[ServiceRegistry] Service '{typeof(T).Name}' not found. " +
                "Ensure it is registered in GameBootstrap before first use.");
            return null;
        }

        /// <summary>Returns <c>true</c> if a service of type <typeparamref name="T"/> is registered.</summary>
        public bool IsRegistered<T>() where T : class => _services.ContainsKey(typeof(T));

        /// <summary>
        /// Removes all registered services.
        /// Use only for testing teardown or full game resets.
        /// </summary>
        public void Clear()
        {
            _services.Clear();
            Debug.Log("[ServiceRegistry] All services cleared.");
        }
    }
}
