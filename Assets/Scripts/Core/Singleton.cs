using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T>
{
    private static T mIns;
    public static T Ins
    {
        get
        {
            if(mIns == null)
            {
                mIns = default(T);
            }

            return mIns;
        }
    }
}
