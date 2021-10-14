using GameEngine;
using System.Text;

public class AgentStateBitTypeQuery : IBitTypeQuery
{
    private StringBuilder fullTypeStr = new StringBuilder();
    private AgentStateBitTypeQuery() { }

    private static AgentStateBitTypeQuery _ins;
    public static AgentStateBitTypeQuery Ins
    {
        get
        {
            if (_ins == null)
            {
                _ins = new AgentStateBitTypeQuery();
            }

            return _ins;
        }
    }



    private void BitTypeEmmurator(BitType bt)
    {
        if (bt.Equals(AgentStateDefine.INTERACT_FLAG))
        {
            fullTypeStr.Append("<可交互>");
        }
        else if (bt.Equals(AgentStateDefine.MOVE_FLAG))
        {
            fullTypeStr.Append("<可运动>");
        }
        else if (bt.Equals(AgentStateDefine.SUPER_ARMOR_FLAG))
        {
            fullTypeStr.Append("<霸体>");
        }       
        else if (bt.Equals(AgentStateDefine.VISIBLE_FLAG))
        {
            fullTypeStr.Append("<可见>");
        }        
        else if (bt.Equals(AgentStateDefine.RECOVER_FLAG))
        {
            fullTypeStr.Append("<可回血>");
        }       
        else if (bt.Equals(AgentStateDefine.HURTABLE_FLAG))
        {
            fullTypeStr.Append("<可受到伤害>");
        }       
        else if (bt.Equals(AgentStateDefine.MAGIC_FLAG))
        {
            fullTypeStr.Append("<可使用技能>");
        }       
        else if (bt.Equals(AgentStateDefine.PHYSICAL_FLAG))
        {
            fullTypeStr.Append("<可物理攻击>");
        }       
        else if (bt.Equals(AgentStateDefine.ITEM_FLAG))
        {
            fullTypeStr.Append("<可使用道具>");
        }       
        else if (bt.Equals(AgentStateDefine.TARGET_FLAG))
        {
            fullTypeStr.Append("<可选择目标>");
        }       
        else if (bt.Equals(AgentStateDefine.MAGIC_IMMUNE_FLAG))
        {
            fullTypeStr.Append("<魔免>");
        }       
        else if (bt.Equals(AgentStateDefine.PHYSICAL_IMMUNE_FLAG))
        {
            fullTypeStr.Append("<物免>");
        }       
    }

    public string BitTypeTranslate(BitType bt)
    {
        if (bt == null)
        {
            Log.Warning("BitTypeTranslate failed,BitType is null");
            return string.Empty;
        }
        fullTypeStr.Clear();
        bt.ForEachSingleType(BitTypeEmmurator);
        return fullTypeStr.ToString();
    }

    public int GetBufferMaxSize()
    {
        return CoreDefine.BitTypeAgentStateBufferSize / CoreDefine.buffeSizeOfInt;
    }
}
