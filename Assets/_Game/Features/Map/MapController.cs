using UnityEngine;
using WesternDeckBuilder.Core;
using WesternDeckBuilder.Models;

namespace WesternDeckBuilder.Features.Map
{
    /// <summary>
    /// Scene controller for the overworld map.
    /// Manages node travel, heat decay, and encounter triggering.
    /// </summary>
    public class MapController : MonoBehaviour
    {
        /// <summary>Heat removed each time the player moves one node away from the heat source.</summary>
        private const int HeatDecayPerNode = 8;
        /// <summary>Additional heat removed when the node is a wilderness type.</summary>
        private const int WildernessHeatBonus = 2;

        private void Start()
        {
            var heat = Services.Heat;
            Debug.Log($"[MapController] Map scene loaded. Heat: {heat?.Heat} (Tier: {heat?.Tier})");
        }

        /// <summary>
        /// Call when the player confirms travel to a node.
        /// Applies heat decay, checks for safe-house clear, and rolls for lawmen encounters.
        /// </summary>
        /// <param name="nodeId">ID of the destination node from NodeData.</param>
        /// <param name="nodeType">Type of the destination node.</param>
        public void OnNodeTraveled(string nodeId, NodeType nodeType)
        {
            var heat = Services.Heat;
            if (heat == null) return;

            // Arriving at a hideout clears heat entirely and skips lawmen roll.
            if (nodeType == NodeType.CampHideout)
            {
                heat.ResetHeat();
                Debug.Log("[MapController] Reached hideout — heat cleared.");
                return;
            }

            // Standard per-node heat decay.
            heat.DecreaseHeat(HeatDecayPerNode);
            if (nodeType == NodeType.WildernessEvent)
                heat.DecreaseHeat(WildernessHeatBonus);

            RollForLawmenEncounter(heat.Tier, nodeId);
        }

        /// <summary>
        /// Manually trigger an encounter for a specific node (e.g. player taps a combat node).
        /// </summary>
        public void StartEncounter(string nodeId)
        {
            Debug.Log($"[MapController] Encounter triggered for node '{nodeId}'.");
            Services.UIFlow?.NavigateTo(SceneNames.Battle);
        }

        // ── Private helpers ────────────────────────────────────────────────────

        private void RollForLawmenEncounter(HeatTier tier, string nodeId)
        {
            float chance = HeatTierUtility.GetLawSpawnChance(tier);
            if (chance <= 0f) return;

            if (Random.value < chance)
            {
                Debug.Log($"[MapController] Lawmen ambush triggered at '{nodeId}' (Tier: {tier}, Chance: {chance:P0}).");
                Services.UIFlow?.NavigateTo(SceneNames.Battle);
            }
        }
    }
}
