public abstract class SkillBase : ISkill
{
    private int _manaConsume;

    public abstract int GetSkillId();

    public int GetManaConsume()
    {
        return _manaConsume;
    }

    public abstract int GetSkillType();

    public abstract void OnSkillCastered(Agent caster, Agent[] targets);
}
