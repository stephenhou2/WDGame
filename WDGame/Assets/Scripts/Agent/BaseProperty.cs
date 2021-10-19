/// <summary>
/// Agent 属性
/// 如果这里产生了大量gc导致性能问题，考虑改为结构体
/// </summary>
public class BaseProperty
{
    /// <summary>
    /// 血量
    /// </summary>
    public int HP;

    /// <summary>
    /// 魔法
    /// </summary>
    public int MP;

    /// <summary>
    /// 血量上限
    /// </summary>
    public int Max_HP;

    /// <summary>
    /// 魔法上限
    /// </summary>
    public int Max_MP;

    /// <summary>
    /// 生命回复
    /// </summary>
    public int HP_Recover;

    /// <summary>
    /// 魔法回复
    /// </summary>
    public int MP_Recover;

    /// <summary>
    /// 物理攻击
    /// </summary>
    public int P_Attack;

    /// <summary>
    /// 魔法攻击
    /// </summary>
    public int M_Attack;

    /// <summary>
    /// 护甲
    /// </summary>
    public int P_Armor;

    /// <summary>
    /// 魔抗
    /// </summary>
    public int M_Armor;

    /// <summary>
    /// 暴击
    /// </summary>
    public int Crit;

}
