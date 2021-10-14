public interface ISkill
{
    int GetSkillId();
    void OnSkillCastered(Agent caster,Agent[] targets);
    int GetSkillType();
}