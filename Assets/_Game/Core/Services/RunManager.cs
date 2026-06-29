using UnityEngine;

namespace WesternDeckBuilder.Core
{
    /// <summary>
    /// Tracks the state and resources of the current run.
    /// Registered by <see cref="GameBootstrap"/> — do not instantiate directly.
    /// </summary>
    public sealed class RunManager : IRunManager
    {
        private int _cash;

        public RunState State { get; private set; } = RunState.None;

        public void StartRun()
        {
            _cash = 0;
            State = RunState.Loadout;
            Debug.Log("[RunManager] Run started.");
        }

        public void EndRun(bool success)
        {
            State = RunState.RunEnd;
            Debug.Log($"[RunManager] Run ended. Success: {success}. Cash earned: {_cash}");
        }

        public void AddCash(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("[RunManager] AddCash called with negative value. Use a positive amount.");
                return;
            }
            _cash += amount;
        }

        public int GetCash() => _cash;
    }
}
