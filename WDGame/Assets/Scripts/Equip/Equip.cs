using System.Collections.Generic;

public class Equip
{
    private int _equipId;
    private BaseProperty _property;
    private List<ISkillEffect> _allSkills = new List<ISkillEffect>();
    
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
        foreach(ISkillEffect skill in _allSkills)
        {
            call(skill);
        }
    }
}
