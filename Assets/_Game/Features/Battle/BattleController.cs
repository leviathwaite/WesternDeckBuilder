using UnityEngine;
using WesternDeckBuilder.Core;
using WesternDeckBuilder.Models;

namespace WesternDeckBuilder.Features.Battle
{
    /// <summary>
    /// Stub scene controller for the Battle scene.
    /// Full combat logic (grit economy, card resolution, enemy intent) is out of scope
    /// for this MVP — this controller proves scene flow, heat handoff, and position state.
    /// <para>
    /// To test flow in the Editor:
    /// <list type="bullet">
    ///   <item>Call <see cref="SimulateWin"/> to return to the map after a won battle.</item>
    ///   <item>Call <see cref="SimulateLoss"/> to end the run and return to loadout.</item>
    /// </list>
    /// </para>
    /// </summary>
    public class BattleController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Encounter type loaded into this battle. Set via scene setup or future BattleDirector.")]
        private EncounterType _encounterType = EncounterType.Bandit;

        private PositionState _playerPosition = PositionState.Exposed;

        private void Start()
        {
            ApplyMissionHeatOnStart();
            Debug.Log($"[BattleController] Battle started. Encounter: {_encounterType}. " +
                      $"Heat: {Services.Heat?.Heat} (Tier: {Services.Heat?.Tier})");
        }

        // ── Public test helpers ────────────────────────────────────────────────

        /// <summary>Simulates a battle win — awards placeholder rewards and returns to the map.</summary>
        public void SimulateWin()
        {
            Debug.Log("[BattleController] Battle won (stub). Rolling rewards and returning to Map.");
            // Reward roll stub — table IDs will come from encounter data in a future phase.
            // Services.Rewards.RollRewards("reward_battle_win");
            Services.UIFlow?.NavigateTo(SceneNames.Map);
        }

        /// <summary>Simulates a battle loss — ends the run and returns to the loadout.</summary>
        public void SimulateLoss()
        {
            Debug.Log("[BattleController] Battle lost (stub). Ending run.");
            Services.Run?.EndRun(false);
            Services.UIFlow?.NavigateTo(SceneNames.Loadout);
        }

        /// <summary>Toggles the player's cover state between Exposed and Covered.</summary>
        public void ToggleCover()
        {
            _playerPosition = _playerPosition == PositionState.Exposed
                ? PositionState.Covered
                : PositionState.Exposed;
            Debug.Log($"[BattleController] Player position: {_playerPosition}");
        }

        // ── Private helpers ────────────────────────────────────────────────────

        private void ApplyMissionHeatOnStart()
        {
            // Train Job stages generate heat on entry; values come from MissionStepData.
            // For now apply a generic heat amount based on encounter type as a placeholder.
            switch (_encounterType)
            {
                case EncounterType.TrainBoarding:
                    Services.Heat?.IncreaseHeat(20);
                    break;
                case EncounterType.TrainInfiltration:
                    Services.Heat?.IncreaseHeat(25);
                    break;
                case EncounterType.TrainSafeDefense:
                    Services.Heat?.IncreaseHeat(30);
                    break;
            }
        }
    }
}
