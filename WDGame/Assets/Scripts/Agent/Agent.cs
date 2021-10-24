using GameEngine;
using UnityEngine;
using System.Collections.Generic;

public abstract class Agent : IAgent
{
    /// <summary>
    /// agent唯一id
    /// </summary>
    protected int _entityId;

    /// <summary>
    /// agent 状态
    /// </summary>
    protected AgentState _state;

    /// <summary>
    /// 装备列表
    /// </summary>
    protected Equip[] _equips = new Equip[EquipDef.MaxEquipNum];

    /// <summary>
    ///  agent 基础属性
    /// </summary>
    protected BaseProperty _property;

    protected Dictionary<int,ISkill> _allSkills = new Dictionary<int, ISkill>();

    public abstract int GetEntityId();
    public abstract int GetAgentType();
    public abstract void OnAlive();
    public abstract void OnDead();
    public abstract void OnLateUpdate(float deltaTime);

    public abstract void OnUpdate(float deltaTime);

    public  void Update(float deltaTime)
    {
        if (_state != null)
        {
            _state.Update(deltaTime);
        }

        OnUpdate(deltaTime);
    }

    public  void LateUpdate(float deltaTime)
    {
        OnLateUpdate(deltaTime);
    }

    public void AddSkill(ISkill skill)
    {
        int skillId = skill.GetSkillId();
        if(_allSkills.ContainsKey(skillId))
        {
            Log.Error(ErrorLevel.Critical,"Readd Skill is not allowed, skill id: {0}", skillId);
            return;
        }

        _allSkills.Add(skillId, skill);
    }

    protected void ForEachSkill(SkillEnumerator call)
    {
        foreach(KeyValuePair <int,ISkill> kv in _allSkills)
        {
            int skillId = kv.Key;
            ISkill skill = kv.Value;
            call(skill);
        }

        for (int i =0;i<_equips.Length;i++)
        {
            var eqp = _equips[i];
            if(eqp != null)
            {
                eqp.ForEachSkill(call);
            }
        }
    }

    public virtual BitType GetAgentState()
    {
        return _state.GetAgentState();
    }

    public virtual void AddState(BitType state)
    {
        _state.AddState(state);
    }

    public virtual void RemoveState(BitType state)
    {
        _state.RemoveState(state);
    }

    public virtual void SetStateTimer(BitType state,AgentStateTimer timer)
    {
        _state.AddStateTimer(state, timer);
    }
}