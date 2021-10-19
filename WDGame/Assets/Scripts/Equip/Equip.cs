using System.Collections.Generic;

public class Equip
{
    private int _equipId;
    private BaseProperty _property;
    private List<ISkill> _allSkills = new List<ISkill>();
    
    public int GetEquipId()
    {
        return _equipId;
    }

    public BaseProperty GetBaseProperty()
    {
        return _property;
    }

    public void ForEachSkill(SkillEnumerator call)
    {
        foreach(ISkill skill in _allSkills)
        {
            call(skill);
        }
    }
}
