public interface ISkill
{
    int GetSkillId();
    int GetSkillType();
    void OnSkillCastered(Agent caster,Agent[] targets);
    void OnSkillFirstAdd(Agent target);
    void Update();
}