using WesternDeckBuilder.Data;

namespace WesternDeckBuilder.Core
{
    /// <summary>
    /// Read-only access to all game data assets loaded by <see cref="DataRegistry"/>.
    /// Resolved via <see cref="Services.Data"/> after bootstrap.
    /// </summary>
    public interface IDataRegistry
    {
        CardData        GetCard(string id);
        WeaponData      GetWeapon(string id);
        KeepsakeData    GetKeepsake(string id);
        EnemyData       GetEnemy(string id);
        NodeData        GetNode(string id);
        MissionStepData GetMissionStep(string id);
        RewardTableData GetRewardTable(string id);
    }
}
