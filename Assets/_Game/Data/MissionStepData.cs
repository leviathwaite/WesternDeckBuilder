using System.Collections.Generic;
using UnityEngine;
using WesternDeckBuilder.Models;

namespace WesternDeckBuilder.Data
{
    /// <summary>
    /// ScriptableObject definition for one stage of a mission (e.g., a Train Job step).
    /// Stages are chained by ID; the mission runner advances through them at runtime.
    /// Create via Assets → Create → WesternDeckBuilder → Data → MissionStep.
    /// Place under Resources/Data/MissionSteps/.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMissionStep", menuName = "WesternDeckBuilder/Data/MissionStep")]
    public class MissionStepData : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _displayName;
        [SerializeField] private string _description;
        [SerializeField] private EncounterType _encounterType;
        [SerializeField] private int _heatGainOnStart;
        [SerializeField] private int _heatGainOnAlarm;
        [SerializeField] private string _nextStepId;
        [SerializeField] private string _failStepId;
        [SerializeField] private List<string> _requirementIds = new List<string>();

        /// <summary>Unique identifier used to chain steps and by the mission runner state machine.</summary>
        public string ID => _id;
        public string DisplayName => _displayName;
        public string Description => _description;
        /// <summary>Combat scenario type to load for this mission step.</summary>
        public EncounterType EncounterType => _encounterType;
        /// <summary>Heat added at the start of this step (e.g., +20 when boarding begins).</summary>
        public int HeatGainOnStart => _heatGainOnStart;
        /// <summary>Additional heat added if an alarm is triggered during this step.</summary>
        public int HeatGainOnAlarm => _heatGainOnAlarm;
        /// <summary>ID of the MissionStepData to advance to on success. Empty = final step.</summary>
        public string NextStepId => _nextStepId;
        /// <summary>ID of the step to fall back to on failure or mission lockout.</summary>
        public string FailStepId => _failStepId;
        /// <summary>Item or condition IDs that must be satisfied before this step can begin.</summary>
        public IReadOnlyList<string> RequirementIds => _requirementIds;
    }
}
