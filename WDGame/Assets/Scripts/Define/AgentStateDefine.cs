using GameEngine;
public static class AgentStateDefine
{
    /// <summary>
    /// 是否可交互
    /// </summary>
    public static BitType INTERACT_FLAG = BitTypeCreator.CreateAgentStateBitType(0);

    /// <summary>
    /// 是否可以运动
    /// </summary>
    public static BitType MOVE_FLAG = BitTypeCreator.CreateAgentStateBitType(1);

    /// <summary>
    /// 是否是霸体
    /// </summary>
    public static BitType SUPER_ARMOR_FLAG = BitTypeCreator.CreateAgentStateBitType(2);

    /// <summary>
    /// 是否可以看到
    /// </summary>
    public static BitType VISIBLE_FLAG = BitTypeCreator.CreateAgentStateBitType(3);

    /// <summary>
    /// 是否能回血
    /// </summary>
    public static BitType RECOVER_FLAG = BitTypeCreator.CreateAgentStateBitType(4);

    /// <summary>
    /// 是否能受到伤害
    /// </summary>
    public static BitType HURTABLE_FLAG = BitTypeCreator.CreateAgentStateBitType(5);

    /// <summary>
    /// 是否能使用技能
    /// </summary>
    public static BitType MAGIC_FLAG = BitTypeCreator.CreateAgentStateBitType(6);

    /// <summary>
    /// 是否能进行物理攻击
    /// </summary>
    public static BitType PHYSICAL_FLAG = BitTypeCreator.CreateAgentStateBitType(7);

    /// <summary>
    /// 是否可以使用道具
    /// </summary>
    public static BitType ITEM_FLAG = BitTypeCreator.CreateAgentStateBitType(8);

    /// <summary>
    /// 是否可以选择目标
    /// </summary>
    public static BitType TARGET_FLAG = BitTypeCreator.CreateAgentStateBitType(9);

    /// <summary>
    /// 是否对魔法伤害免疫
    /// </summary>
    public static BitType MAGIC_IMMUNE_FLAG = BitTypeCreator.CreateAgentStateBitType(10);

    /// <summary>
    /// 是否对物理伤害免疫
    /// </summary>
    public static BitType PHYSICAL_IMMUNE_FLAG = BitTypeCreator.CreateAgentStateBitType(11);


    public static BitType HERO_DEFAULT_STATE = BitType.BindWithBitTypes(new BitType[]
        {
                INTERACT_FLAG,
                MOVE_FLAG,
                VISIBLE_FLAG,
                RECOVER_FLAG,
                HURTABLE_FLAG,
                MAGIC_FLAG,
                PHYSICAL_FLAG,
                TARGET_FLAG,
        }, true);
}
