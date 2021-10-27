using GameEngine;
using System.Collections.Generic;

public class Hero : Agent
{
    private int _heroId;

    public Hero(int entityId, int heroId, HeroData heroData, AgentStateTimerPool stateTimerPool)
    {
        _buffDict = new Dictionary<BitType, IBuff>();
        InitializeHero(entityId, heroId, heroData, stateTimerPool);
    }

    public override void AddBuff(IBuff newBuff)
    {
        if (newBuff == null)
        {
            Log.Error(ErrorLevel.Critical, "AddBuff Error, newBuff is null!");
            return;
        }

        BitType buffType = newBuff.GetBuffType();

        if(_buffDict.TryGetValue(buffType, out IBuff buff))
        {
            if(buff == null)
            {
                Log.Error(ErrorLevel.Normal, "AddBuff Error, agent has null buff,buffType:{0}", newBuff.GetBuffType());
                _buffDict.Add(buffType, newBuff);
            }
            else
            {
                buff.Merge(newBuff);
            }
        }
        else
        {
            _buffDict.Add(buffType, newBuff);
        }
    }

    public override void RemoveBuff(BitType buffType)
    {
        if (buffType == null)
        {
            Log.Error(ErrorLevel.Critical, "RemoveBuff Error, buffType is null!");
            return;
        }

        if (_buffDict.ContainsKey(buffType))
        {
            _buffDict.Remove(buffType);
        }
    }

    public override void CasterSkill(ISkill skill, Agent[] targets)
    {
        throw new System.NotImplementedException();
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
        _buffDict.Clear();
    }

    public override void OnEnterBattle()
    {
        foreach(KeyValuePair<BitType,IBuff> kv in (_buffDict))
        { 
            IBuff buff = kv.Value;
            if (buff != null)
            {
                buff.OnEnterBattle(this);
            }
            else
            {
                Log.Error(ErrorLevel.Normal, "OnEnterBattle Error,agent has null buff!");
            }
        }
    }

    public override void OnExitBattle()
    {
        foreach (KeyValuePair<BitType, IBuff> kv in (_buffDict))
        {
            IBuff buff = kv.Value;
            if (buff != null)
            {
                buff.OnExitBattle(this);
            }
            else
            {
                Log.Error(ErrorLevel.Normal, "OnExitBattle Error,agent has null buff!");
            }
        }
    }

    public override void OnLateUpdate(float deltaTime)
    {
        
    }

    public override void OnUpdate(float deltaTime)
    {
        
    }

}
