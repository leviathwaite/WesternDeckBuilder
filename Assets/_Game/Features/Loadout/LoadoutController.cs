using UnityEngine;
using WesternDeckBuilder.Core;

namespace WesternDeckBuilder.Features.Loadout
{
    /// <summary>
    /// Scene controller for the Loadout screen.
    /// Shows the player's persistent gear before they set off on a run.
    /// <para>
    /// Wire UI buttons to <see cref="OnConfirmLoadout"/> — no other inspector
    /// assignments are required.
    /// </para>
    /// </summary>
    public class LoadoutController : MonoBehaviour
    {
        private void Start()
        {
            var run = Services.Run;
            if (run == null)
            {
                Debug.LogError("[LoadoutController] IRunManager not found. Ensure GameBootstrap ran before this scene.");
                return;
            }
            run.StartRun();
            Debug.Log("[LoadoutController] Loadout scene ready. Awaiting player confirmation.");
        }

        /// <summary>
        /// Called by the Confirm / Start Run UI button.
        /// Advances the flow to the overworld map.
        /// </summary>
        public void OnConfirmLoadout()
        {
            Debug.Log("[LoadoutController] Loadout confirmed — navigating to Map.");
            Services.UIFlow?.NavigateTo(SceneNames.Map);
        }
    }
}
