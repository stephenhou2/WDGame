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

    public override void OnSkillCastered(Agent caster, Agent[] targets)
    {
        
    }

    private IEnumerator PolyMorph(Agent caster, Agent[] targets)
    {
        for(int i =0;i<targets.Length;i++)
        {
            Agent agent = targets[i];
            if(agent != null)
            {
                agent.RemoveState(AgentStateDefine.INTERACT_FLAG);
                agent.RemoveState(AgentStateDefine.MOVE_FLAG);
            }
        }

        yield return new WaitForSeconds(1);


    }

    public override void OnSkillFirstAdd(Agent target)
    {
        _skillCoroutine = null;
    }

    public override void Update()
    {
        
    }
}
