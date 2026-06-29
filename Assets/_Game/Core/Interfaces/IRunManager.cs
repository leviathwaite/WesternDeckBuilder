namespace WesternDeckBuilder.Core
{
    /// <summary>Current phase of an active run.</summary>
    public enum RunState
    {
        None,
        Loadout,
        MapTravel,
        Encounter,
        MissionChain,
        Escape,
        RunEnd
    }

    /// <summary>Tracks the state and resources of the current run.</summary>
    public interface IRunManager
    {
        /// <summary>Current phase of the run.</summary>
        RunState State { get; }
        void StartRun();
        void EndRun(bool success);
        /// <summary>Adds cash to the run's earned total.</summary>
        void AddCash(int amount);
        int GetCash();
    }
}
