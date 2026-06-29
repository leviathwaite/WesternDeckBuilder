using UnityEngine;
using UnityEngine.SceneManagement;

namespace WesternDeckBuilder.Core
{
    /// <summary>
    /// Loads Unity scenes by name.
    /// All scene names are declared as constants in <see cref="SceneNames"/>.
    /// Scenes must be added to Build Settings for navigation to succeed.
    /// </summary>
    public sealed class UIFlowService : IUIFlowService
    {
        private string _previousScene;

        public string CurrentScene => SceneManager.GetActiveScene().name;

        public void NavigateTo(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogError("[UIFlowService] Cannot navigate: scene name is null or empty.");
                return;
            }
            _previousScene = CurrentScene;
            Debug.Log($"[UIFlowService] {_previousScene} → {sceneName}");
            SceneManager.LoadScene(sceneName);
        }

        public void NavigateBack()
        {
            if (string.IsNullOrEmpty(_previousScene))
            {
                Debug.LogWarning("[UIFlowService] No previous scene recorded; cannot navigate back.");
                return;
            }
            NavigateTo(_previousScene);
        }
    }
}
