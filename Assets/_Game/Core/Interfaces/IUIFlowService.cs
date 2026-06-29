namespace WesternDeckBuilder.Core
{
    /// <summary>Handles scene transitions for the full game flow: Boot → Loadout → Map → Battle → Map.</summary>
    public interface IUIFlowService
    {
        /// <summary>Name of the currently active scene.</summary>
        string CurrentScene { get; }
        /// <summary>Loads the scene with the given name. Scene must be registered in Build Settings.</summary>
        void NavigateTo(string sceneName);
        /// <summary>Returns to the previously active scene.</summary>
        void NavigateBack();
    }
}
