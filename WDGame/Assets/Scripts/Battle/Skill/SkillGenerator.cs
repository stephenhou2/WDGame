public static class SkillGenerator
{
    public static SkillBase GenerateSkill(int skillId)
    {
        switch(skillId)
        {
            case 1:
                return new Skill_PolyMorph();
            default:
                return null;
        }
    }
}
