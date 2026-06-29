using System.Collections.Generic;
using UnityEngine;
using WesternDeckBuilder.Models;

namespace WesternDeckBuilder.Data
{
    /// <summary>
    /// ScriptableObject definition for a single overworld map node.
    /// Create via Assets → Create → WesternDeckBuilder → Data → Node.
    /// Place under Resources/Data/Nodes/.
    /// </summary>
    [CreateAssetMenu(fileName = "NewNode", menuName = "WesternDeckBuilder/Data/Node")]
    public class NodeData : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _displayName;
        [SerializeField] private NodeType _nodeType;
        [SerializeField] private string _rewardTableId;
        [SerializeField] private List<string> _connectedNodeIds = new List<string>();

        /// <summary>Unique identifier used by the map graph and save data.</summary>
        public string ID => _id;
        public string DisplayName => _displayName;
        public NodeType NodeType => _nodeType;
        /// <summary>ID of the RewardTableData rolled when the player visits this node.</summary>
        public string RewardTableId => _rewardTableId;
        /// <summary>IDs of nodes directly connected to this one on the county map.</summary>
        public IReadOnlyList<string> ConnectedNodeIds => _connectedNodeIds;
    }
}
