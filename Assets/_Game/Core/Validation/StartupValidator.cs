using UnityEngine;
using WesternDeckBuilder.Data;

namespace WesternDeckBuilder.Core
{
    /// <summary>
    /// Validates loaded data assets at startup.
    /// Runs automatically from <see cref="GameBootstrap"/> after <see cref="DataRegistry.LoadAll"/>.
    /// <para>
    /// Current checks (DataRegistry handles duplicate and missing ID detection internally):
    /// <list type="bullet">
    ///   <item>Confirms the registry loaded without exceptions.</item>
    /// </list>
    /// Add field-level null checks here as schemas mature.
    /// </para>
    /// </summary>
    public static class StartupValidator
    {
        /// <summary>
        /// Runs all startup validation checks against the supplied registry.
        /// All findings are logged to the Unity Console.
        /// </summary>
        public static void Run(IDataRegistry registry)
        {
            if (registry == null)
            {
                Debug.LogError("[StartupValidator] DataRegistry is null — startup validation cannot run.");
                return;
            }

            // DataRegistry.LoadAll() already reports:
            //   - Duplicate IDs  → LogError
            //   - Empty/null IDs → LogWarning
            // Future: iterate each collection and validate required fields (e.g. non-zero GritCost,
            // non-empty DisplayName, valid EffectKey references).

            Debug.Log("[StartupValidator] Startup validation complete. Check above for any warnings/errors.");
        }
    }
}
