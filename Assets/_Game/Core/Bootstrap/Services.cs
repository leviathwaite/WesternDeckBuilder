namespace WesternDeckBuilder.Core
{
    /// <summary>
    /// Static convenience accessors for the most commonly used services.
    /// All properties resolve through <see cref="ServiceRegistry"/> and return
    /// <c>null</c> (with a logged error) if the service was not registered.
    /// <para>
    /// Example:
    /// <code>
    /// Services.Heat.IncreaseHeat(20);
    /// Services.UIFlow.NavigateTo(SceneNames.Map);
    /// </code>
    /// </para>
    /// </summary>
    public static class Services
    {
        /// <summary>Run state tracker — active phase, cash, and run lifecycle.</summary>
        public static IRunManager    Run     => ServiceRegistry.Instance.Resolve<IRunManager>();
        /// <summary>Heat value manager — increase, decrease, reset, and tier query.</summary>
        public static IHeatManager   Heat    => ServiceRegistry.Instance.Resolve<IHeatManager>();
        /// <summary>Scene navigation — Navigate to scene by name or back to previous.</summary>
        public static IUIFlowService UIFlow  => ServiceRegistry.Instance.Resolve<IUIFlowService>();
        /// <summary>Device-aware input event router.</summary>
        public static IInputRouter   Input   => ServiceRegistry.Instance.Resolve<IInputRouter>();
        /// <summary>Reward table roller — returns IDs of rewards to grant.</summary>
        public static IRewardService Rewards => ServiceRegistry.Instance.Resolve<IRewardService>();
        /// <summary>Data asset registry — look up cards, weapons, enemies, etc. by ID.</summary>
        public static IDataRegistry  Data    => ServiceRegistry.Instance.Resolve<IDataRegistry>();
    }
}
