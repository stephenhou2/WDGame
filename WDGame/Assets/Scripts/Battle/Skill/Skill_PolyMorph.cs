/// <summary>
/// 变羊
/// </summary>
public class Skill_PolyMorph : SkillBase
{
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
}
