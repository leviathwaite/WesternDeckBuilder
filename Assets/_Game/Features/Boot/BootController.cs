using UnityEngine;
using WesternDeckBuilder.Core;

namespace WesternDeckBuilder.Features.Boot
{
    /// <summary>
    /// Scene controller for the Boot scene.
    /// <para>
    /// Ensures <see cref="GameBootstrap"/> exists even when entering the Boot scene
    /// directly in the Editor (e.g. pressing Play from within the Boot scene).
    /// In a normal build, GameBootstrap is already present because it is the first
    /// object in the first scene.
    /// </para>
    /// </summary>
    public class BootController : MonoBehaviour
    {
        private void Awake()
        {
            if (FindObjectOfType<GameBootstrap>() == null)
            {
                Debug.Log("[BootController] GameBootstrap not found — creating it now.");
                var go = new GameObject("[GameBootstrap]");
                go.AddComponent<GameBootstrap>();
            }
        }
    }
}
