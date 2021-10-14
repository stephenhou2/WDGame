using GameEngine;
using System.Collections.Generic;

public abstract class Agent : IAgent
{

}


public class Building:Agent
{
    BitType state = BitType.BindBitTypes(new List<BitType>
    {
        AgentStateDefine.INTERACT_FLAG,
        AgentStateDefine.MAGIC_IMMUNE_FLAG,
        AgentStateDefine.PHYSICAL_IMMUNE_FLAG,
        AgentStateDefine.MAGIC_FLAG,
        AgentStateDefine.VISIBLE_FLAG,
    });
}