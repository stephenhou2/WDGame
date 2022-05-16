public interface ISkillEffect : ITriggerble
{
    //void OnTrigger(Agent[] srcs,Agent[] targets);
    //void OnLearn(Agent agt);
    void OnLevelUp(Agent agt, int fromLv, int toLv);
}