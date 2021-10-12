public interface ISkill
{
    int GetSkillId();
    bool OnSkillCaster(Agent caster,Agent[] targets);
    int GetSkillType();
}
