using GameEngine;
using UnityEngine;
using System.Collections.Generic;

public abstract class Agent : IAgent, IBattle
{
    /// <summary>
    /// Agent 唯一id
    /// </summary>
    protected int _entityId;

    /// <summary>
    /// Agent 数据层
    /// </summary>
    protected AgentData _agentData;

    /// <summary>
    /// Agent 状态定时器池子
    /// </summary>
    protected AgentStateTimerPool _stateTimerPool;

    protected Dictionary<BitType,IBuff> _buffDict;

    public int GetEntityId()
    {
        return _entityId;
    }
    public abstract int GetAgentType();
    public abstract void OnAlive();
    public abstract void OnDead();
    public abstract void OnLateUpdate(float deltaTime);
    public abstract void OnUpdate(float deltaTime);
    public abstract void OnEnterBattle();
    public abstract void OnExitBattle();
    public abstract void CasterSkill(ISkill skill,Agent[] targets);
    public abstract void AddBuff(IBuff buff);
    public abstract void RemoveBuff(BitType buffType);


    public  void Update(float deltaTime)
    {
        if (_stateTimerPool != null)
        {
            _stateTimerPool.Update(deltaTime,_agentData.GetAgentStateData());
        }
        OnUpdate(deltaTime);
    }

    public  void LateUpdate(float deltaTime)
    {
        OnLateUpdate(deltaTime);
    }

    public BaseProperty GetBaseProperty()
    {
        if(_agentData == null)
        {
            return null;
        }

        return _agentData.GetAgentBaseProperty();
    }

    public void AddSkill(ISkill skill)
    {
        if(_agentData != null)
        {
            _agentData.AddSkill(skill);
        }
    }

    public void ForEachSkill(SkillEnumerator call)
    {
        if(_agentData != null)
        {
            _agentData.ForEachSkill(call);
        }
    }

    public virtual BitType GetAgentState()
    {
        if (_agentData != null)
        {
            _agentData.GetAgentState();
        }
        return null;
    }

    public virtual void AddState(BitType state)
    {
        if(_agentData != null)
        {
            _agentData.AddState(state);
        }
    }

    public virtual void RemoveState(BitType state)
    {
        if(_agentData != null)
        {
            _agentData.RemoveState(state);
        }
    }  
    
    public virtual void SetStateTimer(BitType state,AgentStateTimer timer)
    {
        if(_stateTimerPool != null)
        {
            _stateTimerPool.SetStateTimer(state,timer);
        }
    }


}