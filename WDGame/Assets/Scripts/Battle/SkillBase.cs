public abstract class SkillBase : ISkill
{
    //private string _skillName;
    private int _manaConsume;

    public abstract int GetSkillId();

    public int GetManaConsume()
    {
        return _manaConsume;
    }

    public abstract int GetSkillType();

    public abstract bool OnSkillCaster(Agent caster, Agent[] targets);
}
