using System;
using System.Collections.Generic;
using UnityEngine;
using WesternDeckBuilder.Core;

namespace WesternDeckBuilder.Data
{
    /// <summary>
    /// Discovers and indexes all game data ScriptableObject assets from the Resources folder.
    /// Call <see cref="LoadAll"/> once during bootstrap before any system queries data.
    /// <para>
    /// Asset placement convention (all paths relative to a Resources/ folder):
    /// <list type="bullet">
    ///   <item>Data/Cards/</item>
    ///   <item>Data/Weapons/</item>
    ///   <item>Data/Keepsakes/</item>
    ///   <item>Data/Enemies/</item>
    ///   <item>Data/Nodes/</item>
    ///   <item>Data/MissionSteps/</item>
    ///   <item>Data/RewardTables/</item>
    /// </list>
    /// </para>
    /// </summary>
    public sealed class DataRegistry : IDataRegistry
    {
        private readonly Dictionary<string, CardData>        _cards        = new Dictionary<string, CardData>();
        private readonly Dictionary<string, WeaponData>      _weapons      = new Dictionary<string, WeaponData>();
        private readonly Dictionary<string, KeepsakeData>    _keepsakes    = new Dictionary<string, KeepsakeData>();
        private readonly Dictionary<string, EnemyData>       _enemies      = new Dictionary<string, EnemyData>();
        private readonly Dictionary<string, NodeData>        _nodes        = new Dictionary<string, NodeData>();
        private readonly Dictionary<string, MissionStepData> _missionSteps = new Dictionary<string, MissionStepData>();
        private readonly Dictionary<string, RewardTableData> _rewardTables = new Dictionary<string, RewardTableData>();

        /// <summary>
        /// Scans all Resources/Data sub-folders and builds the ID-keyed lookup tables.
        /// Duplicate or empty IDs are logged as errors/warnings but do not throw.
        /// </summary>
        public void LoadAll()
        {
            LoadAssets("Data/Cards",        _cards,        a => a.ID);
            LoadAssets("Data/Weapons",      _weapons,      a => a.ID);
            LoadAssets("Data/Keepsakes",    _keepsakes,    a => a.ID);
            LoadAssets("Data/Enemies",      _enemies,      a => a.ID);
            LoadAssets("Data/Nodes",        _nodes,        a => a.ID);
            LoadAssets("Data/MissionSteps", _missionSteps, a => a.ID);
            LoadAssets("Data/RewardTables", _rewardTables, a => a.ID);

            Debug.Log(
                $"[DataRegistry] Loaded: {_cards.Count} cards, {_weapons.Count} weapons, " +
                $"{_keepsakes.Count} keepsakes, {_enemies.Count} enemies, " +
                $"{_nodes.Count} nodes, {_missionSteps.Count} mission steps, " +
                $"{_rewardTables.Count} reward tables.");
        }

        // ── IDataRegistry ──────────────────────────────────────────────────────
        public CardData        GetCard(string id)        => Lookup(_cards,        id, "CardData");
        public WeaponData      GetWeapon(string id)      => Lookup(_weapons,      id, "WeaponData");
        public KeepsakeData    GetKeepsake(string id)    => Lookup(_keepsakes,    id, "KeepsakeData");
        public EnemyData       GetEnemy(string id)       => Lookup(_enemies,      id, "EnemyData");
        public NodeData        GetNode(string id)        => Lookup(_nodes,        id, "NodeData");
        public MissionStepData GetMissionStep(string id) => Lookup(_missionSteps, id, "MissionStepData");
        public RewardTableData GetRewardTable(string id) => Lookup(_rewardTables, id, "RewardTableData");

        // ── Helpers ────────────────────────────────────────────────────────────

        private static void LoadAssets<T>(
            string path,
            Dictionary<string, T> map,
            Func<T, string> idSelector) where T : ScriptableObject
        {
            var assets = Resources.LoadAll<T>(path);
            foreach (var asset in assets)
            {
                var id = idSelector(asset);
                if (string.IsNullOrEmpty(id))
                {
                    Debug.LogWarning(
                        $"[DataRegistry] Asset '{asset.name}' of type {typeof(T).Name} has no ID set. Skipped.");
                    continue;
                }
                if (map.ContainsKey(id))
                {
                    Debug.LogError(
                        $"[DataRegistry] Duplicate ID '{id}' for type {typeof(T).Name}. " +
                        $"Asset '{asset.name}' skipped. Check Resources/{path}/.");
                    continue;
                }
                map[id] = asset;
            }
        }

        private static T Lookup<T>(Dictionary<string, T> map, string id, string typeName) where T : class
        {
            if (string.IsNullOrEmpty(id))
            {
                Debug.LogError($"[DataRegistry] {typeName} lookup called with null or empty ID.");
                return null;
            }
            if (map.TryGetValue(id, out var result))
                return result;
            Debug.LogError($"[DataRegistry] {typeName} not found for id '{id}'.");
            return null;
        }
    }
}
