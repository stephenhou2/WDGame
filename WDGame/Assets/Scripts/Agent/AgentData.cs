using System.Collections.Generic;
using GameEngine;

public abstract class AgentData
{
    /// <summary>
    /// agent 状态数据
    /// </summary>
    protected AgentStateData _agentStateData;

    /// <summary>
    /// 装备列表
    /// </summary>
    protected Equip[] _equips;

    /// <summary>
    ///  agent 基础属性
    /// </summary>
    protected BaseProperty _baseProperty;

    /// <summary>
    /// 所有技能字典
    /// </summary>
    protected Dictionary<int, ISkill> _allSkills;


    public void AddSkill(ISkill skill)
    {
        int skillId = skill.GetSkillId();
        if (_allSkills.ContainsKey(skillId))
        {
            Log.Error(ErrorLevel.Critical, "Readd Skill is not allowed, skill id: {0}", skillId);
            return;
        }

        _allSkills.Add(skillId, skill);
    }

    public void ForEachSkill(SkillEnumerator call)
    {
        foreach (KeyValuePair<int, ISkill> kv in _allSkills)
        {
            int skillId = kv.Key;
            ISkill skill = kv.Value;
            call(skill);
        }

        for (int i = 0; i < _equips.Length; i++)
        {
            var eqp = _equips[i];
            if (eqp != null)
            {
                eqp.ForEachSkill(call);
            }
        }
    }

    public BaseProperty GetAgentBaseProperty()
    {
        return _baseProperty;
    }

    public AgentStateData GetAgentStateData()
    {
        return _agentStateData;
    }

    public virtual BitType GetAgentState()
    {
        return _agentStateData.GetAgentState();
    }

    public virtual void AddState(BitType state)
    {
        _agentStateData.AddState(state);
    }

    public virtual void RemoveState(BitType state)
    {
        _agentStateData.RemoveState(state);
    } 

    public virtual void Dispose()
    {

    }
}
