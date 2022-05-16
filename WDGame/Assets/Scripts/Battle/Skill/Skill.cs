using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    public int SkillId { get; }

    private int mSkillLv;
    public int SkillLv { get { return mSkillLv; } }

    protected SkillEffect[] mSkillEffects;

    //public Skill

    public void ChangeSkillLv(int to)
    {
        mSkillLv = to;
    }

    public abstract void InitSkillEffects();



    public void OnSkillCaster(Agent[] srcs, Agent[] targets)
    {
        //Log.Logic(LogLevel.Hint, "Skill castered")
    }

}
