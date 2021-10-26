using GameEngine;
using System.Collections.Generic;

public class HeroData : AgentData
{
    public HeroData(int heroId)
    {
        _agentStateData = new AgentStateData(AgentStateDefine.HERO_DEFAULT_STATE);
        _equips = new Equip[EquipDef.MaxEquipNum];
        _baseProperty = new BaseProperty();
        _allSkills = new Dictionary<int, ISkill>();
    }
}
