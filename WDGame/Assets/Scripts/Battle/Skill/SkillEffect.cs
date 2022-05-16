public abstract class SkillEffect : ISkillEffect
{
    public abstract int GetSkillEffectId();

    public abstract bool CheckTrigger();

    public abstract void OnTrigger(Agent[] srcs, Agent[] targets);

    //public abstract void OnLearn(Agent agt);

    public abstract void OnLevelUp(Agent agt, int fromLv, int toLv);



}
