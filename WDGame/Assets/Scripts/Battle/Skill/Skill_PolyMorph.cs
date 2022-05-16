using System.Collections;
using GameEngine;

/// <summary>
/// 变羊
/// </summary>
public class Skill_PolyMorph : SkillBase
{
    private IEnumerator _skillCoroutine;

    public override int GetSkillId()
    {
        return SkillDef.SKILL_ID_POLYMORPH;
    }

    public override int GetSkillType()
    {
        return SkillDef.ACTIVE_SKILL;
    }

    public override void OnSkillTrigger(Agent caster, Agent[] targets)
    {
        PolyMorph(caster, targets);
    }

    private void PolyMorph(Agent caster, Agent[] targets)
    {
        for(int i =0;i<targets.Length;i++)
        {
            Agent agent = targets[i];
            if(agent != null)
            {
                agent.RemoveState(AgentStateDefine.INTERACT_FLAG);
                agent.RemoveState(AgentStateDefine.MOVE_FLAG);

                agent.SetStateTimer(AgentStateDefine.INTERACT_FLAG, AgentStateTimer.Create(3));
                agent.SetStateTimer(AgentStateDefine.MOVE_FLAG, AgentStateTimer.Create(5));
            }
        }

    }

    public override void OnSkillFirstAdd(Agent target)
    {
        
    }

    public override void Update()
    {
        
    }
}
