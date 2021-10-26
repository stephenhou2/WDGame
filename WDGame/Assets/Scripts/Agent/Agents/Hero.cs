using GameEngine;

public class Hero : Agent
{
    private int _heroId;

    public Hero(int entityId, int heroId, HeroData heroData, AgentStateTimerPool stateTimerPool)
    {
        InitializeHero(entityId, heroId, heroData, stateTimerPool);
    }

    public override int GetAgentType()
    {
        return AgentTypeDef.AGENT_TYPE_HERO;
    }

    public void InitializeHero(int entityId, int heroId, HeroData heroData, AgentStateTimerPool stateTimerPool)
    {
        _entityId = entityId;
        _heroId = heroId;
        _agentData = heroData;
        _stateTimerPool = stateTimerPool;
    }

    public override void OnAlive()
    {
        // 添加被动技能效果
        ForEachSkill((ISkill skill) =>
        {
            if (skill.GetSkillType() == SkillDef.PASSIVE_SKILL)
            {
                skill.OnSkillFirstAdd(this);
            }
        });
    }

    public override void OnDead()
    {
        
    }

    public override void OnLateUpdate(float deltaTime)
    {
        
    }

    public override void OnUpdate(float deltaTime)
    {
        
    }
}
